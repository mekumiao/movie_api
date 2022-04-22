using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model;

internal class CodeGenerationRegister : ICodeGenerationRegister
{
    public void Register(CodeGenerationConfig config)
    {
        config.AdaptTo("[name]Dto", MapType.Map | MapType.MapToTarget | MapType.Projection)
              .IgnoreNullValues(true)
              .ShallowCopyForSameType(true)
              .IgnoreDataMemberAttribute()
              .ForType<Actor>()
              .ForType<User>()
              .ForType<ApkVersion>()
              .ForType<HostConfig>()
              .ForType<Movie>()
              .ForType<MovieFile>(cfg => cfg.Ignore(x => x.FileFullName))
              .ForType<MovieType>()
              .ForType<UserMovie>()
              .ForType<UserSearchHistory>()
              .ForType<UploadFile>(cfg => cfg.Ignore(x => x.FilePath));

        config.GenerateMapper("[name]Mapper")
              .ForType<MovieFile>()
              .ForType<UserSearchHistory>()
              .ForType<MovieType>()
              .ForType<Actor>()
              .ForType<HostConfig>()
              .ForType<ApkVersion>();
    }
}
