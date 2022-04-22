using System.Linq;

namespace MovieAPI.Model
{
    public static partial class MovieTypeMapper
    {
        public static MovieAPI.Model.MovieTypeDto AdaptToDto(this MovieAPI.DAL.MovieType p1)
        {
            if (p1 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieTypeDto result = new MovieAPI.Model.MovieTypeDto();
            
            result.Count = p1.Movies.Count<MovieAPI.DAL.Movie>();
            
            if (p1.Name != null)
            {
                result.Name = p1.Name;
            }
            
            if (p1.Remark != null)
            {
                result.Remark = p1.Remark;
            }
            result.Id = p1.Id;
            return result;
            
        }
        public static MovieAPI.Model.MovieTypeDto AdaptTo(this MovieAPI.DAL.MovieType p2, MovieAPI.Model.MovieTypeDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieTypeDto result = p3 ?? new MovieAPI.Model.MovieTypeDto();
            
            result.Count = p2.Movies.Count<MovieAPI.DAL.Movie>();
            
            if (p2.Name != null)
            {
                result.Name = p2.Name;
            }
            
            if (p2.Remark != null)
            {
                result.Remark = p2.Remark;
            }
            result.Id = p2.Id;
            return result;
            
        }
        public static System.Linq.Expressions.Expression<System.Func<MovieAPI.DAL.MovieType, MovieAPI.Model.MovieTypeDto>> ProjectToDto => p4 => new MovieAPI.Model.MovieTypeDto()
        {
            Count = p4.Movies.Count<MovieAPI.DAL.Movie>(),
            Name = p4.Name,
            Remark = p4.Remark,
            Id = p4.Id
        };
    }
}