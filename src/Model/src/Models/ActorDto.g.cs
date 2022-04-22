namespace MovieAPI.Model
{
    public partial record ActorDto
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureDiskURL { get; set; }
        public string Remark { get; set; }
        public long Id { get; set; }
    }
}