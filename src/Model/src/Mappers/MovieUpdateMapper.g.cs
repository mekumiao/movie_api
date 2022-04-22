namespace MovieAPI.Model.Mappers
{
    public partial class MovieUpdateMapper : MovieAPI.Model.Mappers.IMovieUpdateMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.Model.MovieUpdate, MovieAPI.DAL.Movie>> Projection => p1 => new MovieAPI.DAL.Movie()
        {
            Remark = p1.Remark,
            ActorName = p1.ActorName
        };
        public MovieAPI.DAL.Movie Map(MovieAPI.Model.MovieUpdate p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.DAL.Movie result = new MovieAPI.DAL.Movie();
            
            if (p2.Remark != null)
            {
                result.Remark = p2.Remark;
            }
            
            if (p2.ActorName != null)
            {
                result.ActorName = p2.ActorName;
            }
            return result;
            
        }
        public MovieAPI.DAL.Movie Map(MovieAPI.Model.MovieUpdate p3, MovieAPI.DAL.Movie p4)
        {
            if (p3 == null)
            {
                return null;
            }
            MovieAPI.DAL.Movie result = p4 ?? new MovieAPI.DAL.Movie();
            
            if (p3.Remark != null)
            {
                result.Remark = p3.Remark;
            }
            
            if (p3.ActorName != null)
            {
                result.ActorName = p3.ActorName;
            }
            return result;
            
        }
    }
}