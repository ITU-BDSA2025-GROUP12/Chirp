namespace Chirp.Razor.Tests;
using Chirp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;

public class CheepRepositoryTests
{
    private CheepRepository GetRepositoryWithData()
    {
        var options = new DbContextOptionsBuilder<ChirpDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ChirpDBContext(options);

        var author = new Author { AuthorId = 1, Name = "Name", Email = "name@example.com" };
        context.Authors.Add(author);
        for (int i = 1; i <= 100; i++)
            context.Cheeps.Add(new Cheep { CheepId = i, Author = author, AuthorId = 1, Text = $"Cheep {i}", TimeStamp = DateTime.Now });

        context.SaveChanges();
        return new CheepRepository(context);
    }
    
    
    [Fact] //pagination test - vi har ikke genindført pagination endnu, but I am on it
    public void GetCheeps_ReturnsPages() {
        var repo = GetRepositoryWithData();

        var result = repo.GetCheeps(page: 2);

        Assert.Equal(32, result.Count);
    }

    [Fact]
    public void GetCheepsFromAuthor_FiltersByName() {
        var repo = GetRepositoryWithData();
        
        var result = repo.GetCheepsFromAuthor("Jacqualine Gilcoine", 1);
        
        Assert.All(result, c => Assert.Equal("Alice", c.Author.Name));
    }

    [Fact]
    public async Task CreateCheep_Throws_IfNoAuthor() {
        var options = new DbContextOptionsBuilder<ChirpDBContext>().UseInMemoryDatabase(databaseName: "NoAuthor").Options;

        var context = new ChirpDBContext(options);
        var repo = new CheepRepository(context);
        var cheep = new Cheep {
            CheepId = 1,
            Text = "This should fail because there is no author",
            TimeStamp = DateTime.UtcNow,
            Author = null
        };

        await Assert.ThrowsAsync<InvalidOperationException>(() => repo.CreateCheep(cheep));
    }
    
    
    
}