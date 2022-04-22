namespace MovieAPI.Model.Mappers
{
    public partial class MovieAddMapper : MovieAPI.Model.Mappers.IMovieAddMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.Model.MovieAdd, MovieAPI.DAL.Movie>> Projection => p1 => new MovieAPI.DAL.Movie()
        {
            DetailRelativeURL = p1.DetailRelativeURL,
            Name = p1.Name,
            Picture = p1.Picture,
            PictureDiskURL = p1.PictureDiskURL,
            Remark = p1.Remark,
            ActorName = p1.ActorName,
            PushTime = p1.PushTime,
            MovieFileName = p1.MovieFileName,
            ResourceLink = p1.ResourceLink,
            MovieTypeName = p1.MovieTypeName
        };
        public MovieAPI.DAL.Movie Map(MovieAPI.Model.MovieAdd p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.DAL.Movie result = new MovieAPI.DAL.Movie();
            
            if (p2.DetailRelativeURL != null)
            {
                result.DetailRelativeURL = p2.DetailRelativeURL;
            }
            
            if (p2.Name != null)
            {
                result.Name = p2.Name;
            }
            
            if (p2.Picture != null)
            {
                result.Picture = p2.Picture;
            }
            
            if (p2.PictureDiskURL != null)
            {
                result.PictureDiskURL = p2.PictureDiskURL;
            }
            
            if (p2.Remark != null)
            {
                result.Remark = p2.Remark;
            }
            
            if (p2.ActorName != null)
            {
                result.ActorName = p2.ActorName;
            }
            
            if (p2.PushTime != null)
            {
                result.PushTime = p2.PushTime;
            }
            
            if (p2.MovieFileName != null)
            {
                result.MovieFileName = p2.MovieFileName;
            }
            
            if (p2.ResourceLink != null)
            {
                result.ResourceLink = p2.ResourceLink;
            }
            
            if (p2.MovieTypeName != null)
            {
                result.MovieTypeName = p2.MovieTypeName;
            }
            return result;
            
        }
        public MovieAPI.DAL.Movie Map(MovieAPI.Model.MovieAdd p3, MovieAPI.DAL.Movie p4)
        {
            if (p3 == null)
            {
                return null;
            }
            MovieAPI.DAL.Movie result = p4 ?? new MovieAPI.DAL.Movie();
            
            if (p3.DetailRelativeURL != null)
            {
                result.DetailRelativeURL = p3.DetailRelativeURL;
            }
            
            if (p3.Name != null)
            {
                result.Name = p3.Name;
            }
            
            if (p3.Picture != null)
            {
                result.Picture = p3.Picture;
            }
            
            if (p3.PictureDiskURL != null)
            {
                result.PictureDiskURL = p3.PictureDiskURL;
            }
            
            if (p3.Remark != null)
            {
                result.Remark = p3.Remark;
            }
            
            if (p3.ActorName != null)
            {
                result.ActorName = p3.ActorName;
            }
            
            if (p3.PushTime != null)
            {
                result.PushTime = p3.PushTime;
            }
            
            if (p3.MovieFileName != null)
            {
                result.MovieFileName = p3.MovieFileName;
            }
            
            if (p3.ResourceLink != null)
            {
                result.ResourceLink = p3.ResourceLink;
            }
            
            if (p3.MovieTypeName != null)
            {
                result.MovieTypeName = p3.MovieTypeName;
            }
            return result;
            
        }
    }
}