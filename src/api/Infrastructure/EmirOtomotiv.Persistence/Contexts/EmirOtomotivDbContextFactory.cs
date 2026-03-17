using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EmirOtomotiv.Persistence.Contexts;

public class EmirOtomotivDbContextFactory : IDesignTimeDbContextFactory<EmirOtomotivDbContext>
{
      public EmirOtomotivDbContext CreateDbContext(string[] args)
        {
            string basePath = Directory.GetCurrentDirectory();
            string apiPath = Path.GetFullPath(Path.Combine(basePath, "..\\..\\Presentation\\EmirOtomotiv.Api"));
            
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(apiPath)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var builder = new DbContextOptionsBuilder<EmirOtomotivDbContext>();
            var connectionString = configuration.GetConnectionString("EmirOtomotivDb") 
                ?? "Host=localhost;Database=EmirOtomotivDb;Username=postgres;Password=postgres";
            
            builder.UseNpgsql(connectionString);

            return new EmirOtomotivDbContext(builder.Options);
        }
}