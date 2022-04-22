namespace MovieAPI.Model.Mappers
{
    public partial class ApkVersionUpdateMapper : MovieAPI.Model.Mappers.IApkVersionUpdateMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.Model.ApkVersionUpdate, MovieAPI.DAL.ApkVersion>> Projection => p1 => new MovieAPI.DAL.ApkVersion() {Remark = p1.Remark};
        public MovieAPI.DAL.ApkVersion Map(MovieAPI.Model.ApkVersionUpdate p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.DAL.ApkVersion result = new MovieAPI.DAL.ApkVersion();
            
            if (p2.Remark != null)
            {
                result.Remark = p2.Remark;
            }
            return result;
            
        }
        public MovieAPI.DAL.ApkVersion Map(MovieAPI.Model.ApkVersionUpdate p3, MovieAPI.DAL.ApkVersion p4)
        {
            if (p3 == null)
            {
                return null;
            }
            MovieAPI.DAL.ApkVersion result = p4 ?? new MovieAPI.DAL.ApkVersion();
            
            if (p3.Remark != null)
            {
                result.Remark = p3.Remark;
            }
            return result;
            
        }
    }
}