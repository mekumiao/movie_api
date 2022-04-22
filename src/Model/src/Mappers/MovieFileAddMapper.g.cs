namespace MovieAPI.Model.Mappers
{
    public partial class MovieFileAddMapper : MovieAPI.Model.Mappers.IMovieFileAddMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.Model.MovieFileAdd, MovieAPI.DAL.MovieFile>> Projection => p1 => new MovieAPI.DAL.MovieFile()
        {
            FileName = p1.FileName,
            FileFullName = p1.FileFullName,
            DiskURL = p1.DiskURL
        };
        public MovieAPI.DAL.MovieFile Map(MovieAPI.Model.MovieFileAdd p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.DAL.MovieFile result = new MovieAPI.DAL.MovieFile();
            
            if (p2.FileName != null)
            {
                result.FileName = p2.FileName;
            }
            
            if (p2.FileFullName != null)
            {
                result.FileFullName = p2.FileFullName;
            }
            
            if (p2.DiskURL != null)
            {
                result.DiskURL = p2.DiskURL;
            }
            return result;
            
        }
        public MovieAPI.DAL.MovieFile Map(MovieAPI.Model.MovieFileAdd p3, MovieAPI.DAL.MovieFile p4)
        {
            if (p3 == null)
            {
                return null;
            }
            MovieAPI.DAL.MovieFile result = p4 ?? new MovieAPI.DAL.MovieFile();
            
            if (p3.FileName != null)
            {
                result.FileName = p3.FileName;
            }
            
            if (p3.FileFullName != null)
            {
                result.FileFullName = p3.FileFullName;
            }
            
            if (p3.DiskURL != null)
            {
                result.DiskURL = p3.DiskURL;
            }
            return result;
            
        }
    }
}