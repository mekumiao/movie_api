using System.Linq;

namespace MovieAPI.Model.Mappers
{
    public partial class UserMapper : MovieAPI.Model.Mappers.IUserMapper
    {
        public System.Linq.Expressions.Expression<System.Func<MovieAPI.DAL.User, MovieAPI.Model.UserDto>> Projection => p1 => new MovieAPI.Model.UserDto()
        {
            RoleNames = p1.Roles != null ? p1.Roles.Select<MovieAPI.DAL.Role, string>(x => x.Name) : null,
            Name = p1.Name,
            FullName = p1.FullName,
            NickName = p1.NickName,
            Gender = p1.Gender,
            Age = p1.Age,
            Picture = p1.Picture,
            Birthday = p1.Birthday,
            Phone = p1.Phone,
            Email = p1.Email,
            Identity = p1.Identity,
            Id = p1.Id
        };
        public MovieAPI.Model.UserDto Map(MovieAPI.DAL.User p2)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.Model.UserDto result = new MovieAPI.Model.UserDto();
            
            if ((p2.Roles != null ? p2.Roles.Select<MovieAPI.DAL.Role, string>(funcMain1) : null) != null)
            {
                result.RoleNames = null;
            }
            
            if (p2.Name != null)
            {
                result.Name = p2.Name;
            }
            
            if (p2.FullName != null)
            {
                result.FullName = p2.FullName;
            }
            
            if (p2.NickName != null)
            {
                result.NickName = p2.NickName;
            }
            result.Gender = p2.Gender;
            result.Age = p2.Age;
            
            if (p2.Picture != null)
            {
                result.Picture = p2.Picture;
            }
            
            if (p2.Birthday != null)
            {
                result.Birthday = p2.Birthday;
            }
            
            if (p2.Phone != null)
            {
                result.Phone = p2.Phone;
            }
            
            if (p2.Email != null)
            {
                result.Email = p2.Email;
            }
            
            if (p2.Identity != null)
            {
                result.Identity = p2.Identity;
            }
            result.Id = p2.Id;
            return result;
            
        }
        public MovieAPI.Model.UserDto Map(MovieAPI.DAL.User p3, MovieAPI.Model.UserDto p4)
        {
            if (p3 == null)
            {
                return null;
            }
            MovieAPI.Model.UserDto result = p4 ?? new MovieAPI.Model.UserDto();
            
            if ((p3.Roles != null ? p3.Roles.Select<MovieAPI.DAL.Role, string>(funcMain1) : null) != null)
            {
                result.RoleNames = null;
            }
            
            if (p3.Name != null)
            {
                result.Name = p3.Name;
            }
            
            if (p3.FullName != null)
            {
                result.FullName = p3.FullName;
            }
            
            if (p3.NickName != null)
            {
                result.NickName = p3.NickName;
            }
            result.Gender = p3.Gender;
            result.Age = p3.Age;
            
            if (p3.Picture != null)
            {
                result.Picture = p3.Picture;
            }
            
            if (p3.Birthday != null)
            {
                result.Birthday = p3.Birthday;
            }
            
            if (p3.Phone != null)
            {
                result.Phone = p3.Phone;
            }
            
            if (p3.Email != null)
            {
                result.Email = p3.Email;
            }
            
            if (p3.Identity != null)
            {
                result.Identity = p3.Identity;
            }
            result.Id = p3.Id;
            return result;
            
        }
        
        private string funcMain1(MovieAPI.DAL.Role x)
        {
            return x.Name;
        }
    }
}