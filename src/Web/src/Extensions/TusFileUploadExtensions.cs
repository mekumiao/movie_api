using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using MovieAPI.DAL;
using MovieAPI.Web;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Stores;

namespace Microsoft.AspNetCore.Builder;

public static class TusFileServerExtensions
{
    public static IApplicationBuilder UseTusFileServer(this IApplicationBuilder app, string directory, string policy)
    {
        return app.Map("/tus", branch =>
        {
            branch.Use((context, next) =>
            {
                SetFileEndpoint(context, policy);
                return next(context);
            });

            branch.UseAuthentication();
            branch.UseAuthorization();

            branch.Use((context, next) =>
            {
                context.SetEndpoint(null);
                return next(context);
            });

            branch.UseTus(httpContext => new DefaultTusConfiguration()
            {
                UrlPath = "/tus",
                Store = new TusDiskStore(directory),
                Events = new Events()
                {
                    OnFileCompleteAsync = async eventContext =>
                    {
                        var uploadFile = await GetUploadFileAsync(eventContext, directory);
                        await SaveToDatabaseAsync(eventContext.HttpContext, uploadFile);
                    },
                },
            });

            branch.UseStaticFiles(new StaticFileOptions
            {
                DefaultContentType = "application/octet-stream",
                ContentTypeProvider = new TusContentTypeProvider(directory),
                FileProvider = new PhysicalFileProvider(directory),
                ServeUnknownFileTypes = true,
            });
        });
    }

    private static void SetFileEndpoint(HttpContext context, string policy)
    {
        var metadata = new List<object> { new AuthorizeAttribute(policy) };
        var endpoint = new Endpoint(
            c => throw new InvalidOperationException("tus"),
            new EndpointMetadataCollection(metadata),
            context.Request.Path);

        context.SetEndpoint(endpoint);
    }

    private static async Task<UploadFile> GetUploadFileAsync(FileCompleteContext eventContext, string directory)
    {
        var file = await eventContext.GetFileAsync();
        var metadatas = await file.GetMetadataAsync(eventContext.CancellationToken);

        var item = new UploadFile();

        if (metadatas.TryGetValue("name", out var name))
        {
            item.UploadName = name.GetString(Encoding.UTF8);
            item.UploadName = StringColumnAttribute.EnsureOkLength(item.UploadName);
            item.Ext = Path.GetExtension(item.UploadName);
        }

        if (metadatas.TryGetValue("remark", out var remark))
        {
            item.Remark = remark.GetString(Encoding.UTF8);
            item.Remark = StringColumnAttribute.EnsureOkLength(item.Remark, 255);
        }

        item.SaveName = file.Id;
        item.FilePath = Path.Combine(directory, file.Id);

        return item;
    }

    private static async Task SaveToDatabaseAsync(HttpContext httpContext, UploadFile uploadFile)
    {
        var user = httpContext.RequestServices.GetRequiredService<IUser>();
        var optionsMonitor = httpContext.RequestServices.GetRequiredService<IOptionsMonitor<AppConfig>>();
        var scopeFactory = httpContext.RequestServices.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
        await dbContext.UploadFiles.AddAsync(uploadFile, httpContext.RequestAborted);
        await dbContext.SaveChangesAsync(httpContext.RequestAborted);
    }
}

public class TusContentTypeProvider : IContentTypeProvider
{
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
    private readonly TusDiskStore _tusDiskStore;

    public TusContentTypeProvider(string directory)
    {
        _tusDiskStore = new TusDiskStore(directory);
        _fileExtensionContentTypeProvider = new FileExtensionContentTypeProvider();
    }

    public bool TryGetContentType(string subpath, [MaybeNullWhen(false)] out string contentType)
    {
        var extension = GetExtension(subpath);

        if (extension != null)
        {
            return _fileExtensionContentTypeProvider.TryGetContentType(subpath, out contentType);
        }

        var fileId = Path.GetFileName(subpath);
        if (_tusDiskStore.FileExistAsync(fileId, CancellationToken.None).Result)
        {
            var file = _tusDiskStore.GetFileAsync(fileId, CancellationToken.None).Result;
            var metadataMap = file.GetMetadataAsync(CancellationToken.None).Result;
            if (metadataMap.TryGetValue("contentType", out var metadata))
            {
                contentType = metadata.GetString(Encoding.UTF8);
                return true;
            }
        }

        contentType = null;
        return false;
    }

    private static string? GetExtension(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        int index = path.LastIndexOf('.');
        if (index < 0)
        {
            return null;
        }

        return path.Substring(index);
    }
}
