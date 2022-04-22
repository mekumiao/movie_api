namespace MovieAPI.Model.Mappers
{
    public partial class HostConfigUpdateMapper : MovieAPI.Model.Mappers.IHostConfigUpdateMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.Model.HostConfigUpdate, MovieAPI.DAL.HostConfig>> Projection => p1 => new MovieAPI.DAL.HostConfig()
        {
            Name = p1.Name,
            Host = p1.Host,
            Remark = p1.Remark
        };
        public MovieAPI.DAL.HostConfig Map(MovieAPI.Model.HostConfigUpdate p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.DAL.HostConfig result = new MovieAPI.DAL.HostConfig();
            
            if (p2.Name != null)
            {
                result.Name = p2.Name;
            }
            
            if (p2.Host != null)
            {
                result.Host = p2.Host;
            }
            
            if (p2.Remark != null)
            {
                result.Remark = p2.Remark;
            }
            return result;
            
        }
        public MovieAPI.DAL.HostConfig Map(MovieAPI.Model.HostConfigUpdate p3, MovieAPI.DAL.HostConfig p4)
        {
            if (p3 == null)
            {
                return null;
            }
            MovieAPI.DAL.HostConfig result = p4 ?? new MovieAPI.DAL.HostConfig();
            
            if (p3.Name != null)
            {
                result.Name = p3.Name;
            }
            
            if (p3.Host != null)
            {
                result.Host = p3.Host;
            }
            
            if (p3.Remark != null)
            {
                result.Remark = p3.Remark;
            }
            return result;
            
        }
    }
}