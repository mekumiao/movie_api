using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using MovieAPI.Web;

namespace Microsoft.AspNetCore.Builder;

public static class MyFileServerExtensions
{
    public static IApplicationBuilder UseMyMovieFileServers(this IApplicationBuilder app, KeyValueConfig[] items, string policy)
    {
        foreach (var item in items)
        {
            app.UseMyFileServer($"/movie/{item.Key}", item.Value, policy, false, new FileExtensionContentTypeProvider(new Dictionary<string, string>
            {
                { ".mp4", "video/mp4" },
            }));
        }
        return app;
    }

    public static IApplicationBuilder UseMyUploadFileServer(this IApplicationBuilder app, string fileDirectory, string policy)
    {
        return app.UseMyFileServer("/upload", fileDirectory, policy, true, null);
    }

    public static IApplicationBuilder UseMyPictureFileServer(this IApplicationBuilder app, string fileDirectory, string policy)
    {
        return app.UseMyFileServer("/picture", fileDirectory, policy, false, new FileExtensionContentTypeProvider(new Dictionary<string, string>
        {
            { ".png", "image/png" },
            { ".pnz", "image/png" },
            { ".jpe", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".jpg", "image/jpeg" },
            { ".ico", "image/x-icon" },
            { ".gif", "image/gif" },
            { ".bmp", "image/bmp" },
        }));
    }

    private static IApplicationBuilder UseMyFileServer(
        this IApplicationBuilder app,
        string requestPath,
        string fileDirectory,
        string policy,
        bool? ServeUnknownFileTypes,
        IContentTypeProvider? ContentTypeProvider)
    {
        return app.Map(requestPath, branch =>
        {
            var files = new PhysicalFileProvider(fileDirectory);

            branch.Use((context, next) =>
            {
                SetFileEndpoint(context, files, policy);
                return next(context);
            });

            branch.UseAuthorization();

            branch.Use((context, next) =>
            {
                var ending = context.GetEndpoint();
                if (ending is null)
                {
                    context.Response.OnStarting(() =>
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        return Task.CompletedTask;
                    });
                    return Task.CompletedTask;
                }
                context.SetEndpoint(null);
                return next(context);
            });

            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = false,
                FileProvider = files,
            };

            options.StaticFileOptions.DefaultContentType = "application/octet-stream";

            if (ContentTypeProvider is not null)
            {
                options.StaticFileOptions.ContentTypeProvider = ContentTypeProvider;
            }

            if (ServeUnknownFileTypes is not null)
            {
                options.StaticFileOptions.ServeUnknownFileTypes = ServeUnknownFileTypes.Value;
            }

            branch.UseFileServer(options);
        });
    }

    private static void SetFileEndpoint(HttpContext context, PhysicalFileProvider files, string policy)
    {
        var fileSystemPath = GetFileSystemPath(files, context.Request.Path);
        if (fileSystemPath != null)
        {
            var metadata = new List<object>
            {
                new DirectoryInfo(Path.GetDirectoryName(fileSystemPath) ?? string.Empty),
                new AuthorizeAttribute(policy)
            };

            var endpoint = new Endpoint(
                c => throw new InvalidOperationException("Static file middleware should return file request."),
                new EndpointMetadataCollection(metadata),
                context.Request.Path);

            context.SetEndpoint(endpoint);
        }
    }

    private static string? GetFileSystemPath(PhysicalFileProvider files, PathString path)
    {
        if (path.HasValue)
        {
            var fileInfo = files.GetFileInfo(path.Value);
            if (fileInfo.Exists)
            {
                return Path.Join(files.Root, path);
            }
        }
        return null;
    }
}
