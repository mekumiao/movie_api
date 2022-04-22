using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IUploadFileUpdateMapper
{
    Expression<Func<UploadFileUpdate, UploadFile>> Projection { get; }
    UploadFile Map(UploadFileUpdate source);
    UploadFile Map(UploadFileUpdate source, UploadFile target);
}
