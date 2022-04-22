using Mapster;
using MovieAPI.Model;
using MovieAPI.Web.Models;

namespace MovieAPI.Web;

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.Default.MaxDepth(1)
                      .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                      .IgnoreNullValues(true);

        config.NewConfig<SchemeHostValue, MovieFileDto>().AfterMapping((src, dest) =>
        {
            if (!string.IsNullOrWhiteSpace(dest.DiskURL))
            {
                dest.URL = $"{src.Value}/movie/{dest.DiskURL.Replace('\\', '/')}";
            }
        });

        config.NewConfig<SchemeHostValue, MovieDto>().AfterMapping((src, dest) =>
        {
            if (!string.IsNullOrWhiteSpace(dest.PictureDiskURL))
            {
                dest.PictureURL = $"{src.Value}/picture/{dest.PictureDiskURL.Replace('\\', '/')}";
            }
            if (dest.MovieFile != null && !string.IsNullOrWhiteSpace(dest.MovieFile.DiskURL))
            {
                dest.MovieFileURL = $"{src.Value}/movie/{dest.MovieFile.DiskURL.Replace('\\', '/')}";
            }
        });

        config.NewConfig<SchemeHostValue, ActorDto>().AfterMapping((src, dest) =>
        {
            if (!string.IsNullOrWhiteSpace(dest.PictureDiskURL))
            {
                dest.PictureURL = $"{src.Value}/picture/{dest.PictureDiskURL.Replace('\\', '/')}";
            }
        });

        config.NewConfig<SchemeHostValue, ApkVersionDto>().AfterMapping((src, dest) =>
        {
            if (!string.IsNullOrWhiteSpace(dest.FileDiskURL))
            {
                dest.FileURL = $"{src.Value}/apk/{dest.FileDiskURL.Replace('\\', '/')}";
            }
        });

        config.NewConfig<SchemeHostValue, UploadFileDto>().AfterMapping((src, dest) =>
        {
            if (!string.IsNullOrWhiteSpace(dest.SaveName))
            {
                dest.URL = $"{src.Value}/upload/{dest.SaveName.Replace('\\', '/')}";
            }
        });
    }
}
