namespace MovieAPI.Model
{
    public partial record MovieDto
    {
        public string Name { get; set; }
        public string PictureDiskURL { get; set; }
        public bool HasPicture { get; set; }
        public string Remark { get; set; }
        public long? ActorId { get; set; }
        public string ActorName { get; set; }
        public System.DateTime? PushTime { get; set; }
        public long? MovieFileId { get; set; }
        public MovieAPI.Model.MovieFileDto? MovieFile { get; set; }
        public string MovieFileName { get; set; }
        public bool HasMovieFile { get; set; }
        public string ResourceLink { get; set; }
        public long? MovieTypeId { get; set; }
        public string MovieTypeName { get; set; }
        public long Id { get; set; }
    }
}