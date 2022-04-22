using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieAPI.Common;
using MovieAPI.DAL;
using MovieAPI.Domain.Core.Bus;
using MovieAPI.Domain.Events;
using MovieAPI.Model;
using MovieAPI.Model.Mappers;
using MovieAPI.Repository;
using MovieAPI.Web.Models;

namespace MovieAPI.Web.Controllers;

/// <summary>
/// 电影资源
/// </summary>
public class MovieController : MyBaseController
{
    private readonly MovieDbContext _dbContext;
    private readonly IUser _user;

    public MovieController(
        MovieDbContext dbContext,
        [FromServices] IUser user)
    {
        _dbContext = dbContext;
        _user = user;
    }

    /// <summary>
    /// 视频信息
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(OutputResult<MovieDto>), StatusCodes.Status200OK)]
    public async Task<MovieDto?> GetAsync(
        [FromServices] IMovieWithUserMovieMapper mapper,
        [FromRoute] long id)
    {
        var movies = _dbContext.Movies.AsNoTracking();
        var userMovies = _dbContext.UserMovies.AsNoTracking();

        var query = (from t1 in movies
                     join t2 in userMovies.Where(x => x.UserId == _user.Id) on t1.Id equals t2.MovieId into JoinedEmpT2
                     from t2 in JoinedEmpT2.DefaultIfEmpty()
                     select new MovieWithUserMovie
                     {
                         Movie = t1,
                         UserMovie = t2,
                     }).Select(mapper.Projection);

        var item = await query.SingleOrDefaultAsync(x => x.Id == id);

        SchemeHostAdaptTo(item);
        SchemeHostAdaptTo(item?.MovieFile);

        return item;
    }

    /// <summary>
    /// 视频列表
    /// </summary>
    /// <param name="eventBus"></param>
    /// <param name="mapper"></param>
    /// <param name="actorId">演员ID</param>
    /// <param name="movieTypeId">电影类型ID</param>
    /// <param name="isDislike">不喜欢</param>
    /// <param name="isStar">收藏</param>
    /// <param name="hasPicture">包含图片</param>
    /// <param name="hasMovieFile">包含电影文件</param>
    /// <param name="distinct">根据电影文件名称去除重复</param>
    /// <param name="hasTotal">返回结果是否包含Total</param>
    /// <param name="q">搜索关键词</param>
    /// <param name="no">页码</param>
    /// <param name="size">页大小</param>
    /// <param name="sorting">排序</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(OutputResult<PaginatedList<MovieDto>>), StatusCodes.Status200OK)]
    public async Task<PaginatedList<MovieDto>> ListAsync(
        [FromServices] IEventBus eventBus,
        [FromServices] IMovieWithUserMovieMapper mapper,
        [FromQuery] long? actorId,
        [FromQuery] long? movieTypeId,
        [FromQuery] bool? isDislike,
        [FromQuery] bool? isStar,
        [FromQuery] bool? hasPicture,
        [FromQuery] bool? hasMovieFile,
        [FromQuery] bool? hasTotal,
        [FromQuery] bool? distinct,
        [FromQuery] int? no,
        [FromQuery] int? size,
        [FromQuery, StringColumn] string? q,
        [FromQuery, StringColumn] string? sorting)
    {
        var movies = _dbContext.Movies.AsNoTracking();
        var userMovies = _dbContext.UserMovies.AsNoTracking();

        if (movieTypeId is not null)
        {
            movies = movies.Where(x => x.MovieTypeId == movieTypeId);
        }

        if (hasPicture is not null)
        {
            movies = movies.Where(x => x.HasPicture == hasPicture);
        }

        if (hasMovieFile is not null)
        {
            movies = movies.Where(x => x.HasMovieFile == hasMovieFile);
        }

        if (actorId is not null)
        {
            movies = movies.Where(x => x.ActorId == actorId);
        }

        var query = (from t1 in movies
                     join t2 in userMovies.Where(x => x.UserId == _user.Id) on t1.Id equals t2.MovieId into JoinedEmpT2
                     from t2 in JoinedEmpT2.DefaultIfEmpty()
                     select new MovieWithUserMovie
                     {
                         Movie = t1,
                         UserMovie = t2,
                     }).Select(mapper.Projection);

        if (!string.IsNullOrWhiteSpace(q))
        {
            eventBus.RaiseEvent<UserSearchEvent>(new UserSearchEvent("Movie", q));

            //这里添加了MovieFileURL为条件，所以需要额外配置映射关系
            query = query.Where(x => x.Name.Contains(q)
                                     || x.MovieTypeName.Contains(q)
                                     || x.Remark.Contains(q)
                                     || x.MovieFileName.Contains(q)
                                     || x.MovieFileURL.Contains(q));
        }

        if (isStar is not null)
        {
            query = query.Where(x => x.IsStar == isStar);
        }

        if (isDislike is not null)
        {
            query = query.Where(x => x.IsDislike == isDislike);
        }

        if (distinct is true)
        {
            // 根据电影文件名称去除重复项
            query = (from t1 in query
                     join t2 in (from it1 in query
                                 group it1 by it1.MovieFileName into gp
                                 select new
                                 {
                                     Id = gp.Min(x => x.Id)
                                 }) on t1.Id equals t2.Id
                     select t1);
        }

        if (!SortingConvert.TryAddSortingsTo(ref query, sorting))
        {
            // 默认排序
            query = query.OrderByDescending(x => x.PushTime).ThenByDescending(x => x.Id);
        }

        var items = await PaginatedList.CreateAfterJumpToAsync(query, no, size, hasTotal);

        SchemeHostAdaptToRange(items);
        SchemeHostAdaptToRange(items.Select(x => x.MovieFile));

        return items;
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> DeleteAsync([FromRoute, Range(1, long.MaxValue)] long id)
    {
        var item = new Movie { Id = id, IsDeleted = false };
        _dbContext.Movies.Attach(item);
        item.IsDeleted = true;
        try
        {
            var result = await _dbContext.SaveChangesAsync();
            return result;
        }
        catch (DbUpdateConcurrencyException)
        {
            return default;
        }
    }

    /// <summary>
    /// 收藏
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isStar"></param>
    /// <returns></returns>
    [HttpPut("{id:long}/[action]")]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> Star(
        [FromRoute, Range(1, long.MaxValue)] long id,
        [FromBody] bool isStar)
    {
        var movie = await _dbContext.Movies.FindAsync(id);
        if (movie is not null)
        {
            var userMovie = await _dbContext.UserMovies.Where(x => x.UserId == _user.Id && x.MovieId == id).SingleOrDefaultAsync();
            if (userMovie is not null)
            {
                userMovie.IsStar = isStar;
            }
            else
            {
                await _dbContext.UserMovies.AddAsync(new() { UserId = _user.Id, MovieId = id, IsStar = isStar });
            }
            return await _dbContext.SaveChangesAsync();
        }
        else
        {
            HttpContext.AddErrorCode(ErrorCodes.NotExists);
            return default;
        }
    }

    /// <summary>
    /// 标记为不喜欢
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isDislike"></param>
    /// <returns></returns>
    [HttpPut("{id:long}/[action]")]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> DislikeAsync(
        [FromRoute, Range(1, long.MaxValue)] long id,
        [FromBody] bool isDislike)
    {
        var movie = await _dbContext.Movies.FindAsync(id);
        if (movie is not null)
        {
            var userMovie = await _dbContext.UserMovies.Where(x => x.UserId == _user.Id && x.MovieId == id)
                                                       .SingleOrDefaultAsync();
            if (userMovie is not null)
            {
                userMovie.IsDislike = isDislike;
            }
            else
            {
                await _dbContext.UserMovies.AddAsync(new() { UserId = _user.Id, MovieId = id, IsDislike = isDislike });
            }
            return await _dbContext.SaveChangesAsync();
        }
        else
        {
            HttpContext.AddErrorCode(ErrorCodes.NotExists);
            return default;
        }
    }

    [HttpPut("{id:long}")]
    [Authorize(Roles = MyConst.Role.Admin)]
    [ProducesResponseType(typeof(OutputResult<int>), StatusCodes.Status200OK)]
    public async Task<int> UpdateAsync(
        [FromServices] UploadFileRepository uploadFileRepository,
        [FromServices] IOptionsMonitor<AppConfig> optionsMonitor,
        [FromServices] IMovieUpdateMapper mapper,
        [FromRoute, Range(1, long.MaxValue)] long id,
        [FromBody] MovieUpdate input)
    {
        var movie = await _dbContext.Movies.FindAsync(id);

        if (movie is not null)
        {
            mapper.Map(input, movie);

            if (input.PictureSaveName is not null)
            {
                var uploadFile = await uploadFileRepository.GetAsync(input.PictureSaveName);
                if (uploadFile is not null)
                {
                    _dbContext.UploadFiles.Remove(uploadFile);
                    movie.PictureDiskURL = $"upload_picture/{uploadFile.SaveName}";
                    var saveDirectory = Path.Combine(optionsMonitor.CurrentValue.PictureDirectory, "upload_picture");
                    var savePath = Path.Combine(optionsMonitor.CurrentValue.PictureDirectory, movie.PictureDiskURL);
                    Directory.CreateDirectory(saveDirectory);
                    System.IO.File.Move(uploadFile.FilePath, savePath);
                    System.IO.File.Delete(uploadFile.FilePath);
                }
            }

            var actor = await _dbContext.Actors.SingleOrDefaultAsync(x => x.Name == input.ActorName);
            if (actor is not null)
            {
                movie.Actor = actor;
            }
            else
            {
                movie.Actor = new Actor
                {
                    Name = input.ActorName,
                };
            }
            return await _dbContext.SaveChangesAsync();
        }
        HttpContext.AddErrorCode(ErrorCodes.NotExists);
        return default;
    }
}
