namespace MovieAPI.Model
{
    public static partial class UserSearchHistoryMapper
    {
        public static MovieAPI.Model.UserSearchHistoryDto AdaptToDto(this MovieAPI.DAL.UserSearchHistory p1)
        {
            if (p1 == null)
            {
                return null;
            }
            MovieAPI.Model.UserSearchHistoryDto result = new MovieAPI.Model.UserSearchHistoryDto();
            
            if (p1.UserId != null)
            {
                result.UserId = p1.UserId;
            }
            
            if (p1.Value != null)
            {
                result.Value = p1.Value;
            }
            
            if (p1.Tag != null)
            {
                result.Tag = p1.Tag;
            }
            result.Id = p1.Id;
            return result;
            
        }
        public static MovieAPI.Model.UserSearchHistoryDto AdaptTo(this MovieAPI.DAL.UserSearchHistory p2, MovieAPI.Model.UserSearchHistoryDto p3)
        {
            if (p2 == null)
            {
                return null;
            }
            MovieAPI.Model.UserSearchHistoryDto result = p3 ?? new MovieAPI.Model.UserSearchHistoryDto();
            
            if (p2.UserId != null)
            {
                result.UserId = p2.UserId;
            }
            
            if (p2.Value != null)
            {
                result.Value = p2.Value;
            }
            
            if (p2.Tag != null)
            {
                result.Tag = p2.Tag;
            }
            result.Id = p2.Id;
            return result;
            
        }
        public static System.Linq.Expressions.Expression<System.Func<MovieAPI.DAL.UserSearchHistory, MovieAPI.Model.UserSearchHistoryDto>> ProjectToDto => p4 => new MovieAPI.Model.UserSearchHistoryDto()
        {
            UserId = p4.UserId,
            Value = p4.Value,
            Tag = p4.Tag,
            Id = p4.Id
        };
    }
}