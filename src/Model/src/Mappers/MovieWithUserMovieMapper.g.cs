namespace MovieAPI.Model.Mappers
{
    public partial class MovieWithUserMovieMapper : MovieAPI.Model.Mappers.IMovieWithUserMovieMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.Model.MovieWithUserMovie, MovieAPI.Model.MovieDto>> Projection => p1 => new MovieAPI.Model.MovieDto()
        {
            MovieFileURL = p1.Movie.MovieFile != null ? p1.Movie.MovieFile.DiskURL : null,
            IsStar = p1.UserMovie != null ? p1.UserMovie.IsStar : false,
            IsDislike = p1.UserMovie != null ? p1.UserMovie.IsDislike : false,
            Name = p1.Movie.Name,
            PictureDiskURL = p1.Movie.PictureDiskURL,
            HasPicture = p1.Movie.HasPicture,
            Remark = p1.Movie.Remark,
            ActorId = p1.Movie.ActorId,
            ActorName = p1.Movie.ActorName,
            PushTime = p1.Movie.PushTime,
            MovieFileId = p1.Movie.MovieFileId,
            MovieFile = p1.Movie.MovieFile == null ? null : new MovieAPI.Model.MovieFileDto()
            {
                FileName = p1.Movie.MovieFile.FileName,
                DiskURL = p1.Movie.MovieFile.DiskURL,
                Id = p1.Movie.MovieFile.Id
            },
            MovieFileName = p1.Movie.MovieFileName,
            HasMovieFile = p1.Movie.HasMovieFile,
            ResourceLink = p1.Movie.ResourceLink,
            MovieTypeId = p1.Movie.MovieTypeId,
            MovieTypeName = p1.Movie.MovieTypeName,
            Id = p1.Movie.Id
        };
        public MovieAPI.Model.MovieDto Map(MovieAPI.Model.MovieWithUserMovie p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieDto result = new MovieAPI.Model.MovieDto();
            
            if ((p2.Movie.MovieFile != null ? p2.Movie.MovieFile.DiskURL : null) != null)
            {
                result.MovieFileURL = p2.Movie.MovieFile != null ? p2.Movie.MovieFile.DiskURL : null;
            }
            result.IsStar = p2.UserMovie != null ? p2.UserMovie.IsStar : false;
            result.IsDislike = p2.UserMovie != null ? p2.UserMovie.IsDislike : false;
            
            if ((p2.Movie == null ? null : p2.Movie.Name) != null)
            {
                result.Name = p2.Movie == null ? null : p2.Movie.Name;
            }
            
            if ((p2.Movie == null ? null : p2.Movie.PictureDiskURL) != null)
            {
                result.PictureDiskURL = p2.Movie == null ? null : p2.Movie.PictureDiskURL;
            }
            
            if ((p2.Movie == null ? null : (bool?)p2.Movie.HasPicture) != null)
            {
                result.HasPicture = funcMain1(p2.Movie == null ? null : (bool?)p2.Movie.HasPicture);
            }
            
            if ((p2.Movie == null ? null : p2.Movie.Remark) != null)
            {
                result.Remark = p2.Movie == null ? null : p2.Movie.Remark;
            }
            
            if ((p2.Movie == null ? null : p2.Movie.ActorId) != null)
            {
                result.ActorId = p2.Movie == null ? null : p2.Movie.ActorId;
            }
            
            if ((p2.Movie == null ? null : p2.Movie.ActorName) != null)
            {
                result.ActorName = p2.Movie == null ? null : p2.Movie.ActorName;
            }
            
            if ((p2.Movie == null ? null : p2.Movie.PushTime) != null)
            {
                result.PushTime = p2.Movie == null ? null : p2.Movie.PushTime;
            }
            
            if ((p2.Movie == null ? null : p2.Movie.MovieFileId) != null)
            {
                result.MovieFileId = p2.Movie == null ? null : p2.Movie.MovieFileId;
            }
            
            if ((p2.Movie == null ? null : p2.Movie.MovieFile) != null)
            {
                result.MovieFile = funcMain2(p2.Movie == null ? null : p2.Movie.MovieFile);
            }
            
            if ((p2.Movie == null ? null : p2.Movie.MovieFileName) != null)
            {
                result.MovieFileName = p2.Movie == null ? null : p2.Movie.MovieFileName;
            }
            
            if ((p2.Movie == null ? null : (bool?)p2.Movie.HasMovieFile) != null)
            {
                result.HasMovieFile = funcMain3(p2.Movie == null ? null : (bool?)p2.Movie.HasMovieFile);
            }
            
            if ((p2.Movie == null ? null : p2.Movie.ResourceLink) != null)
            {
                result.ResourceLink = p2.Movie == null ? null : p2.Movie.ResourceLink;
            }
            
            if ((p2.Movie == null ? null : p2.Movie.MovieTypeId) != null)
            {
                result.MovieTypeId = p2.Movie == null ? null : p2.Movie.MovieTypeId;
            }
            
            if ((p2.Movie == null ? null : p2.Movie.MovieTypeName) != null)
            {
                result.MovieTypeName = p2.Movie == null ? null : p2.Movie.MovieTypeName;
            }
            
            if ((p2.Movie == null ? null : (long?)p2.Movie.Id) != null)
            {
                result.Id = funcMain4(p2.Movie == null ? null : (long?)p2.Movie.Id);
            }
            return result;
            
        }
        public MovieAPI.Model.MovieDto Map(MovieAPI.Model.MovieWithUserMovie p7, MovieAPI.Model.MovieDto p8)
        {
            if (p7 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieDto result = p8 ?? new MovieAPI.Model.MovieDto();
            
            if ((p7.Movie.MovieFile != null ? p7.Movie.MovieFile.DiskURL : null) != null)
            {
                result.MovieFileURL = p7.Movie.MovieFile != null ? p7.Movie.MovieFile.DiskURL : null;
            }
            result.IsStar = p7.UserMovie != null ? p7.UserMovie.IsStar : false;
            result.IsDislike = p7.UserMovie != null ? p7.UserMovie.IsDislike : false;
            
            if ((p7.Movie == null ? null : p7.Movie.Name) != null)
            {
                result.Name = p7.Movie == null ? null : p7.Movie.Name;
            }
            
            if ((p7.Movie == null ? null : p7.Movie.PictureDiskURL) != null)
            {
                result.PictureDiskURL = p7.Movie == null ? null : p7.Movie.PictureDiskURL;
            }
            
            if ((p7.Movie == null ? null : (bool?)p7.Movie.HasPicture) != null)
            {
                result.HasPicture = funcMain5(p7.Movie == null ? null : (bool?)p7.Movie.HasPicture, result.HasPicture);
            }
            
            if ((p7.Movie == null ? null : p7.Movie.Remark) != null)
            {
                result.Remark = p7.Movie == null ? null : p7.Movie.Remark;
            }
            
            if ((p7.Movie == null ? null : p7.Movie.ActorId) != null)
            {
                result.ActorId = p7.Movie == null ? null : p7.Movie.ActorId;
            }
            
            if ((p7.Movie == null ? null : p7.Movie.ActorName) != null)
            {
                result.ActorName = p7.Movie == null ? null : p7.Movie.ActorName;
            }
            
            if ((p7.Movie == null ? null : p7.Movie.PushTime) != null)
            {
                result.PushTime = p7.Movie == null ? null : p7.Movie.PushTime;
            }
            
            if ((p7.Movie == null ? null : p7.Movie.MovieFileId) != null)
            {
                result.MovieFileId = p7.Movie == null ? null : p7.Movie.MovieFileId;
            }
            
            if ((p7.Movie == null ? null : p7.Movie.MovieFile) != null)
            {
                result.MovieFile = funcMain6(p7.Movie == null ? null : p7.Movie.MovieFile, result.MovieFile);
            }
            
            if ((p7.Movie == null ? null : p7.Movie.MovieFileName) != null)
            {
                result.MovieFileName = p7.Movie == null ? null : p7.Movie.MovieFileName;
            }
            
            if ((p7.Movie == null ? null : (bool?)p7.Movie.HasMovieFile) != null)
            {
                result.HasMovieFile = funcMain7(p7.Movie == null ? null : (bool?)p7.Movie.HasMovieFile, result.HasMovieFile);
            }
            
            if ((p7.Movie == null ? null : p7.Movie.ResourceLink) != null)
            {
                result.ResourceLink = p7.Movie == null ? null : p7.Movie.ResourceLink;
            }
            
            if ((p7.Movie == null ? null : p7.Movie.MovieTypeId) != null)
            {
                result.MovieTypeId = p7.Movie == null ? null : p7.Movie.MovieTypeId;
            }
            
            if ((p7.Movie == null ? null : p7.Movie.MovieTypeName) != null)
            {
                result.MovieTypeName = p7.Movie == null ? null : p7.Movie.MovieTypeName;
            }
            
            if ((p7.Movie == null ? null : (long?)p7.Movie.Id) != null)
            {
                result.Id = funcMain8(p7.Movie == null ? null : (long?)p7.Movie.Id, result.Id);
            }
            return result;
            
        }
        
        private bool funcMain1(bool? p3)
        {
            return p3 == null ? false : (bool)p3;
        }
        
        private MovieAPI.Model.MovieFileDto funcMain2(MovieAPI.DAL.MovieFile p4)
        {
            if (p4 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieFileDto result = new MovieAPI.Model.MovieFileDto();
            
            if (p4.FileName != null)
            {
                result.FileName = p4.FileName;
            }
            
            if (p4.DiskURL != null)
            {
                result.DiskURL = p4.DiskURL;
            }
            result.Id = p4.Id;
            return result;
            
        }
        
        private bool funcMain3(bool? p5)
        {
            return p5 == null ? false : (bool)p5;
        }
        
        private long funcMain4(long? p6)
        {
            return p6 == null ? 0l : (long)p6;
        }
        
        private bool funcMain5(bool? p9, bool p10)
        {
            return p9 == null ? false : (bool)p9;
        }
        
        private MovieAPI.Model.MovieFileDto funcMain6(MovieAPI.DAL.MovieFile p11, MovieAPI.Model.MovieFileDto p12)
        {
            if (p11 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieFileDto result = p12 ?? new MovieAPI.Model.MovieFileDto();
            
            if (p11.FileName != null)
            {
                result.FileName = p11.FileName;
            }
            
            if (p11.DiskURL != null)
            {
                result.DiskURL = p11.DiskURL;
            }
            result.Id = p11.Id;
            return result;
            
        }
        
        private bool funcMain7(bool? p13, bool p14)
        {
            return p13 == null ? false : (bool)p13;
        }
        
        private long funcMain8(long? p15, long p16)
        {
            return p15 == null ? 0l : (long)p15;
        }
    }
}