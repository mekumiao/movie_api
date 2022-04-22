namespace MovieAPI.Model
{
    public static partial class HostConfigMapper
    {
        public static MovieAPI.Model.HostConfigDto AdaptToDto(this MovieAPI.DAL.HostConfig p1)
        {
            if (p1 == null)
            {
                return null;
            }
            MovieAPI.Model.HostConfigDto result = new MovieAPI.Model.HostConfigDto();
            
            if (p1.Name != null)
            {
                result.Name = p1.Name;
            }
            
            if (p1.Host != null)
            {
                result.Host = p1.Host;
            }
            
            if (p1.Remark != null)
            {
                result.Remark = p1.Remark;
            }
            result.Id = p1.Id;
            return result;
            
        }
        public static MovieAPI.Model.HostConfigDto AdaptTo(this MovieAPI.DAL.HostConfig p2, MovieAPI.Model.HostConfigDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.Model.HostConfigDto result = p3 ?? new MovieAPI.Model.HostConfigDto();
            
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
            result.Id = p2.Id;
            return result;
            
        }
        public static System.Linq.Expressions.Expression<System.Func<MovieAPI.DAL.HostConfig, MovieAPI.Model.HostConfigDto>> ProjectToDto => p4 => new MovieAPI.Model.HostConfigDto()
        {
            Name = p4.Name,
            Host = p4.Host,
            Remark = p4.Remark,
            Id = p4.Id
        };
    }
}