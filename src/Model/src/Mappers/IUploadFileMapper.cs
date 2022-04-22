using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IUploadFileMapper
{
    Expression<Func<UploadFile, UploadFileDto>> Projection { get; }
    UploadFileDto Map(UploadFile source);
    UploadFileDto Map(UploadFile source, UploadFileDto target);
}
