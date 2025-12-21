namespace Chirp.Razor.Tests;

using Chirp.Core1;
using Chirp.Infrastructure.Data;
using Chirp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Threading.Tasks;

public class CheepRepositoryTests
{
    private CheepRepository GetRepositoryWithData()
    {
        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ChirpDBContext(options);

        var author = new Author
        {
            Id = 1,
            FirstName = "Name",
            Email = "name@example.com",
            UserName = "name@example.com"
        };

        context.Authors.Add(author);

        for (int i = 1; i <= 100; i++)
        {
            context.Cheeps.Add(new Cheep
            {
                Id = i,
                AuthorId = author.Id,
                Author = author,
                Text = $"Cheep {i}",
                TimeStamp = DateTime.UtcNow
            });
        }

        context.SaveChanges();

        return new CheepRepository(context, null!);
    }

    [Fact]
    public void GetCheepsFromAuthor_FiltersByName()
    {
        var repo = GetRepositoryWithData();

        var result = repo.GetCheepsFromAuthor("Name", 1);

        Assert.All(result, c => Assert.Equal("Name", c.Author.FirstName));
    }

    [Fact]
    public async Task CreateCheep_Throws_IfUserManagerIsMissing()
    {
        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ChirpDBContext(options);

        var repo = new CheepRepository(context, null!);

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await repo.CreateCheep("This should fail", "noauthor@example.com");
        });
    }
}
