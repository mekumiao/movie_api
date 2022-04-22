using System.Linq.Expressions;
using Mapster;
using MovieAPI.DAL;

namespace MovieAPI.Model.Mappers;

[Mapper]
public interface IApkVersionUpdateMapper
{
    Expression<Func<ApkVersionUpdate, ApkVersion>> Projection { get; }
    ApkVersion Map(ApkVersionUpdate source);
    ApkVersion Map(ApkVersionUpdate source, ApkVersion target);
}
