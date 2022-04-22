using Mapster;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using MovieAPI.Common;
using MovieAPI.Common.Converters;
using MovieAPI.DAL;
using MovieAPI.Domain.Core.Bus;
using MovieAPI.Domain.EventHandlers;
using MovieAPI.Domain.Events;
using MovieAPI.Repository;
using MovieAPI.Web;
using MovieAPI.Web.Auths;
using MovieAPI.Web.Filters;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

#region Configure配置
builder.Services.Configure<AppConfig>(builder.Configuration);
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("ConnectionStrings"));
#endregion

var config = builder.Configuration.Get<AppConfig>();

#region 日志配置
builder.Logging.ClearProviders();
builder.Host.UseSerilog(configureLogger: (host, logger) =>
{
    logger.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
    logger.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information);
    logger.MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning);
    logger.MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles", LogEventLevel.Warning);
    var filepath = "logs/log-.log";
    var template = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}";
    if (host.HostingEnvironment.IsDevelopment())
    {
        logger.MinimumLevel.Debug();
    }
    logger.WriteTo.File(filepath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30, outputTemplate: template);
    logger.WriteTo.Console(outputTemplate: template);
});
#endregion

#region 认证方案和授权策略配置
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddDevelopment()
                .AddPassword()
                .AddAuthorizationCode()
                .AddCookie(options =>
                {
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return Task.CompletedTask;
                    };
                });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("movie", options => options.RequireAuthenticatedUser());
    options.AddPolicy("upload", options => options.RequireAuthenticatedUser());
    options.AddPolicy("picture", options => options.RequireAuthenticatedUser());
    options.AddPolicy("api", options =>
    {
        options.RequireAuthenticatedUser();
        if (builder.Environment.IsDevelopment())
        {
            // 开发模式下添加DevelopmentAuthentication认证方案的认证结果到该授权策略
            options.AddAuthenticationSchemes(DevelopmentAuthenticationDefaults.AuthenticationScheme);
        }
        options.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
    });
    options.AddPolicy("tus", options =>
    {
        options.RequireAuthenticatedUser()
               .AddAuthenticationSchemes(AuthorizationCodeAuthenticationDefaults.AuthenticationScheme)
               .AddAuthenticationSchemes(PasswordAuthenticationDefaults.AuthenticationScheme)
               .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
    });
    options.DefaultPolicy = options.GetPolicy("api")!;
});
#endregion

#region HttpClient配置
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUser, MovieAPI.Web.User>();
#endregion

#region DbContext配置
var connectionString = builder.Configuration.GetConnectionString("MariadbConnectionString");
var version = builder.Configuration.GetConnectionString("MariadbVersion");

builder.Services.AddDbContext<MovieDbContext>(options =>
{
    options.UseExceptionProcessorMySql();

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();

        options.UseMySql(
            connectionString,
            new MariaDbServerVersion(version),
            options => options.MigrationsAssembly("MovieAPI.MovieMigrations.Development")
                              .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }
    else if (builder.Environment.IsProduction())
    {
        options.UseMySql(
            connectionString,
            new MariaDbServerVersion(version),
            options => options.MigrationsAssembly("MovieAPI.MovieMigrations"));
    }
});
#endregion

#region 跨域配置
var origins = builder.Configuration.GetSection("Origins").GetChildren().Select(x => x.Value).ToArray();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithExposedHeaders(tusdotnet.Helpers.CorsHelper.GetExposedHeaders());
    });
});
#endregion

#region Mapster配置
TypeAdapterConfig.GlobalSettings.Apply(new MovieAPI.Services.MappingRegister());
TypeAdapterConfig.GlobalSettings.Apply(new MovieAPI.Model.MappingRegister());
TypeAdapterConfig.GlobalSettings.Apply(new MovieAPI.Web.MappingRegister());
TypeAdapterConfig.GlobalSettings.Compile();

builder.Services.Scan(selector => selector.FromAssemblyOf<MovieAPI.Model.MappingRegister>()
                                          .AddClasses(classes => classes.InNamespaces(new[] { "MovieAPI.Model.Mappers" }))
                                          .AsMatchingInterface()
                                          .WithSingletonLifetime());

builder.Services.Scan(selector => selector.FromAssemblyOf<MovieAPI.Services.MappingRegister>()
                                          .AddClasses(classes => classes.InNamespaces(new[] { "MovieAPI.Services.Mappers" }))
                                          .AsMatchingInterface()
                                          .WithSingletonLifetime());
#endregion

#region Http请求体大小限制的配置
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = config.MaxRequestBodySize;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = config.MaxRequestBodySize;
});
#endregion

#region Controller相关配置
builder.Services.AddControllers(options =>
{
    options.Filters.Add<PaginatedTotalResultFilterAttribute>();
    options.Filters.Add<OutputResultFilterAttribute>();
    options.Filters.Add<GlobalExceptionFilterAttribute>();
    options.MaxModelValidationErrors = 10;//模型验证最大错误数
    options.MaxValidationDepth = 20;//最大递归验证层数
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.MaxDepth = 20;
    options.JsonSerializerOptions.PropertyNamingPolicy = default;
    options.JsonSerializerOptions.DictionaryKeyPolicy = default;
    options.JsonSerializerOptions.Converters.Add(new LongJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new ULongJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new ShortTimeJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new ByteArrayJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new DateTimeToTimestampJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetToTimestampJsonConverter());
});
#endregion

#region Swagger配置
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieAPI", Version = "v1" });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MovieAPI.Common.xml"), true);
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MovieAPI.Model.xml"), true);
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MovieAPI.Web.xml"), true);
});
#endregion

#region EventBus配置
builder.Services.AddEventBus<UserSearchEventHandler, UserSearchEvent>();
builder.Services.AddEventBus<ApkDownloadEventHandler, ApkDownloadEvent>();
#endregion

#region Repository配置
builder.Services.AddScoped<UploadFileRepository>();
builder.Services.AddScoped<AccountRepository>();
#endregion

#region 其他配置
builder.Services.AddSingleton(new Snowflake(5, 5));
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

#region Swagger中间件
app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    x.RoutePrefix = "swagger";
    x.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});
#endregion

app.UseCors();

#region 公开访问的静态资源
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true });
app.UseStaticFiles(new StaticFileOptions
{
    RequestPath = "/apk",
    FileProvider = new PhysicalFileProvider(app.Configuration.Get<AppConfig>().ApkDirectory),
    DefaultContentType = "application/octet-stream",
    ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
    {
        { ".apk", "application/vnd.android.package-archive" }
    }),
    OnPrepareResponse = (context) =>
    {
        if (context.File.Exists)
        {
            context.Context.RequestServices.GetRequiredService<IEventBus>()
                                           .RaiseEvent(new ApkDownloadEvent(context.File.Name));
        }
    }
});
#endregion

// 确保执行默认的认证方案
app.UseAuthentication();

#region 受保护的静态资源
app.UseMyUploadFileServer(config.UploadDirectory, "upload");
app.UseMyPictureFileServer(config.PictureDirectory, "picture");
app.UseMyMovieFileServers(config.Movies, "movie");
app.UseTusFileServer(config.TusDirectory, "tus");
#endregion

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

#pragma warning disable CA1050 // 在命名空间中声明类型
public partial class Program { }
