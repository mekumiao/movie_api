namespace MovieAPI.Model
{
    public partial record UserMovieDto
    {
        public long? UserId { get; set; }
        public long? MovieId { get; set; }
        public bool IsDislike { get; set; }
        public bool IsStar { get; set; }
        public long Id { get; set; }
    }
}