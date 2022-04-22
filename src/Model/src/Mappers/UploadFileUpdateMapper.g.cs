namespace MovieAPI.Model.Mappers
{
    public partial class UploadFileUpdateMapper : MovieAPI.Model.Mappers.IUploadFileUpdateMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.Model.UploadFileUpdate, MovieAPI.DAL.UploadFile>> Projection => p1 => new MovieAPI.DAL.UploadFile() {Remark = p1.Remark};
        public MovieAPI.DAL.UploadFile Map(MovieAPI.Model.UploadFileUpdate p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.DAL.UploadFile result = new MovieAPI.DAL.UploadFile();
            
            if (p2.Remark != null)
            {
                result.Remark = p2.Remark;
            }
            return result;
            
        }
        public MovieAPI.DAL.UploadFile Map(MovieAPI.Model.UploadFileUpdate p3, MovieAPI.DAL.UploadFile p4)
        {
            if (p3 == null)
            {
                return null;
            }
            MovieAPI.DAL.UploadFile result = p4 ?? new MovieAPI.DAL.UploadFile();
            
            if (p3.Remark != null)
            {
                result.Remark = p3.Remark;
            }
            return result;
            
        }
    }
}