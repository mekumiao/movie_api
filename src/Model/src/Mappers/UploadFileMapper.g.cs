namespace MovieAPI.Model.Mappers
{
    public partial class UploadFileMapper : MovieAPI.Model.Mappers.IUploadFileMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.DAL.UploadFile, MovieAPI.Model.UploadFileDto>> Projection => p1 => new MovieAPI.Model.UploadFileDto()
        {
            SaveName = p1.SaveName,
            UploadName = p1.UploadName,
            Remark = p1.Remark,
            IsDisabled = p1.IsDisabled,
            Ext = p1.Ext,
            CreateUserId = p1.CreateUserId,
            UpdateUserId = p1.UpdateUserId,
            CreateTime = p1.CreateTime,
            UpdateTime = p1.UpdateTime,
            CreateUserFullName = p1.CreateUser.FullName,
            UpdateUserFullName = p1.UpdateUser.FullName
        };
        public MovieAPI.Model.UploadFileDto Map(MovieAPI.DAL.UploadFile p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.Model.UploadFileDto result = new MovieAPI.Model.UploadFileDto();
            
            if (p2.SaveName != null)
            {
                result.SaveName = p2.SaveName;
            }
            
            if (p2.UploadName != null)
            {
                result.UploadName = p2.UploadName;
            }
            
            if (p2.Remark != null)
            {
                result.Remark = p2.Remark;
            }
            result.IsDisabled = p2.IsDisabled;
            
            if (p2.Ext != null)
            {
                result.Ext = p2.Ext;
            }
            
            if (p2.CreateUserId != null)
            {
                result.CreateUserId = p2.CreateUserId;
            }
            
            if (p2.UpdateUserId != null)
            {
                result.UpdateUserId = p2.UpdateUserId;
            }
            
            if (p2.CreateTime != null)
            {
                result.CreateTime = p2.CreateTime;
            }
            
            if (p2.UpdateTime != null)
            {
                result.UpdateTime = p2.UpdateTime;
            }
            
            if ((p2.CreateUser == null ? null : p2.CreateUser.FullName) != null)
            {
                result.CreateUserFullName = p2.CreateUser == null ? null : p2.CreateUser.FullName;
            }
            
            if ((p2.UpdateUser == null ? null : p2.UpdateUser.FullName) != null)
            {
                result.UpdateUserFullName = p2.UpdateUser == null ? null : p2.UpdateUser.FullName;
            }
            return result;
            
        }
        public MovieAPI.Model.UploadFileDto Map(MovieAPI.DAL.UploadFile p3, MovieAPI.Model.UploadFileDto p4)
        {
            if (p3 == null)
            {
                return null;
            }
            MovieAPI.Model.UploadFileDto result = p4 ?? new MovieAPI.Model.UploadFileDto();
            
            if (p3.SaveName != null)
            {
                result.SaveName = p3.SaveName;
            }
            
            if (p3.UploadName != null)
            {
                result.UploadName = p3.UploadName;
            }
            
            if (p3.Remark != null)
            {
                result.Remark = p3.Remark;
            }
            result.IsDisabled = p3.IsDisabled;
            
            if (p3.Ext != null)
            {
                result.Ext = p3.Ext;
            }
            
            if (p3.CreateUserId != null)
            {
                result.CreateUserId = p3.CreateUserId;
            }
            
            if (p3.UpdateUserId != null)
            {
                result.UpdateUserId = p3.UpdateUserId;
            }
            
            if (p3.CreateTime != null)
            {
                result.CreateTime = p3.CreateTime;
            }
            
            if (p3.UpdateTime != null)
            {
                result.UpdateTime = p3.UpdateTime;
            }
            
            if ((p3.CreateUser == null ? null : p3.CreateUser.FullName) != null)
            {
                result.CreateUserFullName = p3.CreateUser == null ? null : p3.CreateUser.FullName;
            }
            
            if ((p3.UpdateUser == null ? null : p3.UpdateUser.FullName) != null)
            {
                result.UpdateUserFullName = p3.UpdateUser == null ? null : p3.UpdateUser.FullName;
            }
            return result;
            
        }
    }
}