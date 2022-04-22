namespace MovieAPI.Model
{
    public partial record UserDto
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public MovieAPI.DAL.Gender Gender { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }
        public System.DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Identity { get; set; }
        public long Id { get; set; }
    }
}