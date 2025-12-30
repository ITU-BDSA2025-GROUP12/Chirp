using Chirp.Infrastructure;
using Chirp.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Chirp.Core1;
using Chirp.Infrastructure.DBContext;

namespace Chirp.Infrastructure.Tests;

public class DBInitializerTests
{
    public ChirpDBContext CreateInMemoryDatabase(){
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection).Options;
        var context = new ChirpDBContext(options);
        context.Database.EnsureCreated();
        return context;
    }
    
    [Fact]
    public void databaseNotEmpty() {
        using var context = CreateInMemoryDatabase();
        
        DbInitializer.SeedDatabase(context);
        
        Assert.True((bool)context.Authors.Any()); //checks if there is at least 1 author
        Assert.True((bool)context.Cheeps.Any()); //checks if there is at least 1 cheep
    }

    [Fact]
    public void dataBaseHasCorrectAuthors() {
        using var context = CreateInMemoryDatabase();

        DbInitializer.SeedDatabase(context);
        
        //checks context has specific authors 
        Assert.True((bool)context.Authors.Any(a => a.FirstName == "Helge"));
        Assert.True((bool)context.Authors.Any(a => a.FirstName == "Adrian"));
        Assert.True((bool)context.Authors.Any(a => a.FirstName == "Jacqualine Gilcoine"));
        Assert.True((bool)context.Authors.Any(a => a.FirstName == "Octavio Wagganer"));
        Assert.True((bool)context.Authors.Any(a => a.FirstName == "Quintin Sitts"));
    }

    [Fact]
    public void dataBaseHasCorrectCheeps() {
        using var context = CreateInMemoryDatabase();

        DbInitializer.SeedDatabase(context);

        //Checks context has specific cheeps
        Assert.True((bool)context.Cheeps.Any(c =>
            c.Text ==
            "They were married in Chicago, with old Smith, and was expected aboard every day; meantime, the two went past me."));
        Assert.True((bool)context.Cheeps.Any(c => c.Text == "Hej, velkommen til kurset."));
        Assert.True((bool)context.Cheeps.Any(c => c.Text == "Hello, BDSA students!"));
        Assert.True((bool)context.Cheeps.Any(c => c.Text == "Starbuck now is what we hear the worst."));
        Assert.True((bool)context.Cheeps.Any(c => c.Text == "It is he, then?"));
    }
}