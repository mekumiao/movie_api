using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieAPI.DAL;

namespace MovieAPI.Repository;

public class UploadFileRepository : IRepository
{
    private readonly IServiceScope serviceScope;
    private readonly MovieDbContext dbContext;
    private bool disposedValue;

    public UploadFileRepository(IServiceScopeFactory serviceScopeFactory)
    {
        serviceScope = serviceScopeFactory.CreateScope();
        dbContext = serviceScope.ServiceProvider.GetRequiredService<MovieDbContext>();
    }

    public async Task<UploadFile?> GetAsync(string saveName)
    {
        var uploadFile = await dbContext.UploadFiles.FindAsync(saveName);
        if (uploadFile is not null)
        {
            if (!File.Exists(uploadFile.FilePath))
            {
                dbContext.Remove(uploadFile);
                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return null;
            }
        }
        return uploadFile;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                dbContext.Dispose();
                serviceScope.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
