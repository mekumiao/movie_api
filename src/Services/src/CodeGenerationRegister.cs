using Mapster;
using MovieAPI.Model;

namespace MovieAPI.Services;

internal class CodeGenerationRegister : ICodeGenerationRegister
{
    public void Register(CodeGenerationConfig config)
    {
        config.AdaptTo("[name]Dto", MapType.Map | MapType.MapToTarget | MapType.Projection)
              .IgnoreNullValues(true)
              .ShallowCopyForSameType(true)
              .IgnoreDataMemberAttribute();

        config.GenerateMapper("[name]Mapper");
    }
}
