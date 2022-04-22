namespace MovieAPI.Domain.Events;

public class ApkDownloadEvent : EventArgs
{
    public string FileName { get; set; }

    public ApkDownloadEvent(string fileName)
    {
        FileName = fileName;
    }
}
