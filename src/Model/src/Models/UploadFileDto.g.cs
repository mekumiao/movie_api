namespace MovieAPI.Model
{
    public partial record UploadFileDto
    {
        public string SaveName { get; set; }
        public string UploadName { get; set; }
        public string Remark { get; set; }
        public bool IsDisabled { get; set; }
        public string Ext { get; set; }
    }
}