namespace MovieAPI.Model.Mappers
{
    public partial class ActorAddMapper : MovieAPI.Model.Mappers.IActorAddMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.Model.ActorAdd, MovieAPI.DAL.Actor>> Projection => p1 => new MovieAPI.DAL.Actor()
        {
            Name = p1.Name,
            Remark = p1.Remark
        };
        public MovieAPI.DAL.Actor Map(MovieAPI.Model.ActorAdd p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.DAL.Actor result = new MovieAPI.DAL.Actor();
            
            if (p2.Name != null)
            {
                result.Name = p2.Name;
            }
            
            if (p2.Remark != null)
            {
                result.Remark = p2.Remark;
            }
            return result;
            
        }
        public MovieAPI.DAL.Actor Map(MovieAPI.Model.ActorAdd p3, MovieAPI.DAL.Actor p4)
        {
            if (p3 == null)
            {
                return null;
            }
            MovieAPI.DAL.Actor result = p4 ?? new MovieAPI.DAL.Actor();
            
            if (p3.Name != null)
            {
                result.Name = p3.Name;
            }
            
            if (p3.Remark != null)
            {
                result.Remark = p3.Remark;
            }
            return result;
            
        }
    }
}