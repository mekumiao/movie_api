namespace MovieAPI.Model
{
    public partial record MovieTypeDto
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public long Id { get; set; }
    }
}