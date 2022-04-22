namespace MovieAPI.Model
{
    public static partial class ActorMapper
    {
        public static MovieAPI.Model.ActorDto AdaptToDto(this MovieAPI.DAL.Actor p1)
        {
            if (p1 == null)
            {
                return null;
            }
            MovieAPI.Model.ActorDto result = new MovieAPI.Model.ActorDto();
            
            if (p1.Name != null)
            {
                result.Name = p1.Name;
            }
            
            if (p1.Picture != null)
            {
                result.Picture = p1.Picture;
            }
            
            if (p1.PictureDiskURL != null)
            {
                result.PictureDiskURL = p1.PictureDiskURL;
            }
            
            if (p1.Remark != null)
            {
                result.Remark = p1.Remark;
            }
            result.Id = p1.Id;
            return result;
            
        }
        public static MovieAPI.Model.ActorDto AdaptTo(this MovieAPI.DAL.Actor p2, MovieAPI.Model.ActorDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.Model.ActorDto result = p3 ?? new MovieAPI.Model.ActorDto();
            
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
            result.Id = p2.Id;
            return result;
            
        }
        public static System.Linq.Expressions.Expression<System.Func<MovieAPI.DAL.Actor, MovieAPI.Model.ActorDto>> ProjectToDto => p4 => new MovieAPI.Model.ActorDto()
        {
            Name = p4.Name,
            Picture = p4.Picture,
            PictureDiskURL = p4.PictureDiskURL,
            Remark = p4.Remark,
            Id = p4.Id
        };
    }
}