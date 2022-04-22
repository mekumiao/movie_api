namespace MovieAPI.Model
{
    public partial record ApkVersionDto
    {
        public string Name { get; set; }
        public int VersionCode { get; set; }
        public string VersionName { get; set; }
        public string FileDiskURL { get; set; }
        public string Remark { get; set; }
        public bool IsActived { get; set; }
        public int Downloads { get; set; }
        public long Id { get; set; }
    }
}