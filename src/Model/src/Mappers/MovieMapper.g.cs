namespace MovieAPI.Model.Mappers
{
    public partial class MovieMapper : MovieAPI.Model.Mappers.IMovieMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.DAL.Movie, MovieAPI.Model.MovieDto>> Projection => p1 => new MovieAPI.Model.MovieDto()
        {
            MovieFileURL = p1.MovieFile != null ? p1.MovieFile.DiskURL : null,
            Name = p1.Name,
            PictureDiskURL = p1.PictureDiskURL,
            HasPicture = p1.HasPicture,
            Remark = p1.Remark,
            ActorId = p1.ActorId,
            ActorName = p1.ActorName,
            PushTime = p1.PushTime,
            MovieFileId = p1.MovieFileId,
            MovieFile = p1.MovieFile == null ? null : new MovieAPI.Model.MovieFileDto()
            {
                FileName = p1.MovieFile.FileName,
                DiskURL = p1.MovieFile.DiskURL,
                Id = p1.MovieFile.Id
            },
            MovieFileName = p1.MovieFileName,
            HasMovieFile = p1.HasMovieFile,
            ResourceLink = p1.ResourceLink,
            MovieTypeId = p1.MovieTypeId,
            MovieTypeName = p1.MovieTypeName,
            Id = p1.Id
        };
        public MovieAPI.Model.MovieDto Map(MovieAPI.DAL.Movie p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieDto result = new MovieAPI.Model.MovieDto();
            
            if ((p2.MovieFile != null ? p2.MovieFile.DiskURL : null) != null)
            {
                result.MovieFileURL = p2.MovieFile != null ? p2.MovieFile.DiskURL : null;
            }
            
            if (p2.Name != null)
            {
                result.Name = p2.Name;
            }
            
            if (p2.PictureDiskURL != null)
            {
                result.PictureDiskURL = p2.PictureDiskURL;
            }
            result.HasPicture = p2.HasPicture;
            
            if (p2.Remark != null)
            {
                result.Remark = p2.Remark;
            }
            
            if (p2.ActorId != null)
            {
                result.ActorId = p2.ActorId;
            }
            
            if (p2.ActorName != null)
            {
                result.ActorName = p2.ActorName;
            }
            
            if (p2.PushTime != null)
            {
                result.PushTime = p2.PushTime;
            }
            
            if (p2.MovieFileId != null)
            {
                result.MovieFileId = p2.MovieFileId;
            }
            
            if (p2.MovieFile != null)
            {
                result.MovieFile = funcMain1(p2.MovieFile);
            }
            
            if (p2.MovieFileName != null)
            {
                result.MovieFileName = p2.MovieFileName;
            }
            result.HasMovieFile = p2.HasMovieFile;
            
            if (p2.ResourceLink != null)
            {
                result.ResourceLink = p2.ResourceLink;
            }
            
            if (p2.MovieTypeId != null)
            {
                result.MovieTypeId = p2.MovieTypeId;
            }
            
            if (p2.MovieTypeName != null)
            {
                result.MovieTypeName = p2.MovieTypeName;
            }
            result.Id = p2.Id;
            return result;
            
        }
        public MovieAPI.Model.MovieDto Map(MovieAPI.DAL.Movie p4, MovieAPI.Model.MovieDto p5)
        {
            if (p4 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieDto result = p5 ?? new MovieAPI.Model.MovieDto();
            
            if ((p4.MovieFile != null ? p4.MovieFile.DiskURL : null) != null)
            {
                result.MovieFileURL = p4.MovieFile != null ? p4.MovieFile.DiskURL : null;
            }
            
            if (p4.Name != null)
            {
                result.Name = p4.Name;
            }
            
            if (p4.PictureDiskURL != null)
            {
                result.PictureDiskURL = p4.PictureDiskURL;
            }
            result.HasPicture = p4.HasPicture;
            
            if (p4.Remark != null)
            {
                result.Remark = p4.Remark;
            }
            
            if (p4.ActorId != null)
            {
                result.ActorId = p4.ActorId;
            }
            
            if (p4.ActorName != null)
            {
                result.ActorName = p4.ActorName;
            }
            
            if (p4.PushTime != null)
            {
                result.PushTime = p4.PushTime;
            }
            
            if (p4.MovieFileId != null)
            {
                result.MovieFileId = p4.MovieFileId;
            }
            
            if (p4.MovieFile != null)
            {
                result.MovieFile = funcMain2(p4.MovieFile, result.MovieFile);
            }
            
            if (p4.MovieFileName != null)
            {
                result.MovieFileName = p4.MovieFileName;
            }
            result.HasMovieFile = p4.HasMovieFile;
            
            if (p4.ResourceLink != null)
            {
                result.ResourceLink = p4.ResourceLink;
            }
            
            if (p4.MovieTypeId != null)
            {
                result.MovieTypeId = p4.MovieTypeId;
            }
            
            if (p4.MovieTypeName != null)
            {
                result.MovieTypeName = p4.MovieTypeName;
            }
            result.Id = p4.Id;
            return result;
            
        }
        
        private MovieAPI.Model.MovieFileDto funcMain1(MovieAPI.DAL.MovieFile p3)
        {
            if (p3 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieFileDto result = new MovieAPI.Model.MovieFileDto();
            
            if (p3.FileName != null)
            {
                result.FileName = p3.FileName;
            }
            
            if (p3.DiskURL != null)
            {
                result.DiskURL = p3.DiskURL;
            }
            result.Id = p3.Id;
            return result;
            
        }
        
        private MovieAPI.Model.MovieFileDto funcMain2(MovieAPI.DAL.MovieFile p6, MovieAPI.Model.MovieFileDto p7)
        {
            if (p6 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieFileDto result = p7 ?? new MovieAPI.Model.MovieFileDto();
            
            if (p6.FileName != null)
            {
                result.FileName = p6.FileName;
            }
            
            if (p6.DiskURL != null)
            {
                result.DiskURL = p6.DiskURL;
            }
            result.Id = p6.Id;
            return result;
            
        }
    }
}