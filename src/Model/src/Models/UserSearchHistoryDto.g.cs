namespace MovieAPI.Model
{
    public partial record UserSearchHistoryDto
    {
        public long? UserId { get; set; }
        public string Value { get; set; }
        public string Tag { get; set; }
        public long Id { get; set; }
    }
}