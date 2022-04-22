namespace MovieAPI.Model
{
    public partial record MovieFileDto
    {
        public string FileName { get; set; }
        public string DiskURL { get; set; }
        public long Id { get; set; }
    }
}