namespace MovieAPI.Model
{
    public static partial class MovieFileMapper
    {
        public static MovieAPI.Model.MovieFileDto AdaptToDto(this MovieAPI.DAL.MovieFile p1)
        {
            if (p1 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieFileDto result = new MovieAPI.Model.MovieFileDto();
            
            if (p1.FileName != null)
            {
                result.FileName = p1.FileName;
            }
            
            if (p1.DiskURL != null)
            {
                result.DiskURL = p1.DiskURL;
            }
            result.Id = p1.Id;
            return result;
            
        }
        public static MovieAPI.Model.MovieFileDto AdaptTo(this MovieAPI.DAL.MovieFile p2, MovieAPI.Model.MovieFileDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.Model.MovieFileDto result = p3 ?? new MovieAPI.Model.MovieFileDto();
            
            if (p2.FileName != null)
            {
                result.FileName = p2.FileName;
            }
            
            if (p2.DiskURL != null)
            {
                result.DiskURL = p2.DiskURL;
            }
            result.Id = p2.Id;
            return result;
            
        }
        public static System.Linq.Expressions.Expression<System.Func<MovieAPI.DAL.MovieFile, MovieAPI.Model.MovieFileDto>> ProjectToDto => p4 => new MovieAPI.Model.MovieFileDto()
        {
            FileName = p4.FileName,
            DiskURL = p4.DiskURL,
            Id = p4.Id
        };
    }
}