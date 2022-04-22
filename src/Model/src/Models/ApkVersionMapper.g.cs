namespace MovieAPI.Model
{
    public static partial class ApkVersionMapper
    {
        public static MovieAPI.Model.ApkVersionDto AdaptToDto(this MovieAPI.DAL.ApkVersion p1)
        {
            if (p1 == null)
            {
                return null;
            }
            MovieAPI.Model.ApkVersionDto result = new MovieAPI.Model.ApkVersionDto();
            
            if (p1.Name != null)
            {
                result.Name = p1.Name;
            }
            result.VersionCode = p1.VersionCode;
            
            if (p1.VersionName != null)
            {
                result.VersionName = p1.VersionName;
            }
            
            if (p1.FileDiskURL != null)
            {
                result.FileDiskURL = p1.FileDiskURL;
            }
            
            if (p1.Remark != null)
            {
                result.Remark = p1.Remark;
            }
            result.IsActived = p1.IsActived;
            result.Downloads = p1.Downloads;
            result.Id = p1.Id;
            return result;
            
        }
        public static MovieAPI.Model.ApkVersionDto AdaptTo(this MovieAPI.DAL.ApkVersion p2, MovieAPI.Model.ApkVersionDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.Model.ApkVersionDto result = p3 ?? new MovieAPI.Model.ApkVersionDto();
            
            if (p2.Name != null)
            {
                result.Name = p2.Name;
            }
            result.VersionCode = p2.VersionCode;
            
            if (p2.VersionName != null)
            {
                result.VersionName = p2.VersionName;
            }
            
            if (p2.FileDiskURL != null)
            {
                result.FileDiskURL = p2.FileDiskURL;
            }
            
            if (p2.Remark != null)
            {
                result.Remark = p2.Remark;
            }
            result.IsActived = p2.IsActived;
            result.Downloads = p2.Downloads;
            result.Id = p2.Id;
            return result;
            
        }
        public static System.Linq.Expressions.Expression<System.Func<MovieAPI.DAL.ApkVersion, MovieAPI.Model.ApkVersionDto>> ProjectToDto => p4 => new MovieAPI.Model.ApkVersionDto()
        {
            Name = p4.Name,
            VersionCode = p4.VersionCode,
            VersionName = p4.VersionName,
            FileDiskURL = p4.FileDiskURL,
            Remark = p4.Remark,
            IsActived = p4.IsActived,
            Downloads = p4.Downloads,
            Id = p4.Id
        };
    }
}