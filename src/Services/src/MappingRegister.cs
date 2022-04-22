using Mapster;

namespace MovieAPI.Services;

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.Default.MaxDepth(1)
                      .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                      .IgnoreNullValues(true);
    }
}
