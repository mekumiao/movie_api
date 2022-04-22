namespace MovieAPI.Model
{
    public partial record HostConfigDto
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Remark { get; set; }
        public long Id { get; set; }
    }
}