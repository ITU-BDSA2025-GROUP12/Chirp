namespace Chirp.Razor.Tests;
using Chirp.Infrastructure.Data;
using Chirp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Chirp.Core1;


public class CheepRepositoryTests
{
    private CheepRepository GetRepositoryWithData()
    {
        var options = new DbContextOptionsBuilder<ChirpDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ChirpDBContext(options);

        var author = new Author { Id = 1, FirstName = "Name", Email = "name@example.com" };
        context.Authors.Add(author);
        for (int i = 1; i <= 100; i++)
            context.Cheeps.Add(new Cheep { CheepId = i, Author = author, Id = 1, Text = $"Cheep {i}", TimeStamp = DateTime.Now });

        context.SaveChanges();
        return new CheepRepository(context);
    }
    
    
    //[Fact] //pagination test - vi har ikke genindført pagination endnu, but I am on it
   /* public void GetCheeps_ReturnsPages() {
        var repo = GetRepositoryWithData();

        var result = repo.GetCheeps(page: 2);

        Assert.Equal(32, result.Count);
    }*/

    [Fact]
    public void GetCheepsFromAuthor_FiltersByName() {
        var repo = GetRepositoryWithData();
        
        var result = repo.GetCheepsFromAuthor("Jacqualine Gilcoine", 1);
        
        Assert.All(result, c => Assert.Equal("Alice", c.Author.FirstName));
    }

    [Fact]
    public void CreateCheep_Throws_IfNoAuthor() {
        var options = new DbContextOptionsBuilder<ChirpDBContext>().UseInMemoryDatabase(databaseName: "NoAuthor").Options;

        var context = new ChirpDBContext(options);
        var repo = new CheepRepository(context);
        var cheep = new Cheep {
            CheepId = 1,
            Text = "This should fail because there is no author",
            TimeStamp = DateTime.UtcNow,
            Author = null
        };

        Assert.Throws<NullReferenceException>(() => repo.CreateCheep(cheep.Text, cheep.Author.Email));
    }
    
    
    
}