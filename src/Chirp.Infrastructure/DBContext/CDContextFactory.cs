using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Chirp.Infrastructure.Data;
/// <summary>
/// Makes sure ChirpDBContext gets constructed correctly
/// </summary>
public class ChirpDBContextFactory
    : IDesignTimeDbContextFactory<ChirpDBContext>
{
    public ChirpDBContext CreateDbContext(string[] args)
    {
        // Build config manually
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ChirpDBContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlite(connectionString);

        return new ChirpDBContext(optionsBuilder.Options);
    }
}