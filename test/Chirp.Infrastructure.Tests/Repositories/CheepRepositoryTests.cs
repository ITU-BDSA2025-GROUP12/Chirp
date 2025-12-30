using Chirp.Infrastructure.Data;
using System.Data.Common;
using Chirp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Chirp.Core1;
using Chirp.Infrastructure.DBContext;

namespace Chirp.Infrastructure.Tests.Repositories;
	

public class CheepRepositoryTests
{
	private DbConnection connection;
	private DbContextOptions<ChirpDBContext> options;
	
	public ChirpDBContext CreateInMemoryDatabase(){
		var connection = new SqliteConnection("DataSource=:memory:");
		connection.Open();

		var options = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection).Options;
		var context = new ChirpDBContext(options);
		context.Database.EnsureCreated();
		return context;
	}

	public ChirpDBContext GetContextWithData() {
		var context = CreateInMemoryDatabase();
		
		var author1 = new Author
		{
			FirstName = "John",
			Email = "example1@itu.dk",
			UserName = "Johnyboi"
		};
		var author2 = new Author
		{
			FirstName = "Bob",
			Email = "example2@itu.dk",
			UserName = "Bobby"
		};
		var cheep1 = new Cheep
		{
			Id = 1,
			Author = author1,
			Text = "this is author 1's cheep",
			TimeStamp = DateTime.Now,
		};
		var cheep2 = new Cheep
		{
			Id = 2,
			Author = author2,
			Text = "this is author 2's cheep",
			TimeStamp = DateTime.Now,
		};
	    
		context.Cheeps.AddRange(cheep1, cheep2);
		context.SaveChanges();
		return context;
	}

	private CheepRepository GetRepositoryWithData()
    {
	    connection = new SqliteConnection("Filename=:memory:");
	    connection.Open(); 
	    options = new DbContextOptionsBuilder<ChirpDBContext>()
	              .UseSqlite(connection)
	              .Options;
	    
   		var context = new ChirpDBContext(options);
		context.Database.EnsureCreated();
        var author = new Author { Id = 1, FirstName = "Name", Email = "name@example.com" };
        context.Authors.Add(author);
        for (int i = 1; i <= 100; i++)
            context.Cheeps.Add(new Cheep { Id = i, Author = author, AuthorId = 1, Text = $"Cheep {i}", TimeStamp = DateTime.Now });

        context.SaveChanges();
        return new CheepRepository(context);
    }

    [Fact]
    public void GetCheepsFromAuthor_FiltersByName() {
        var repo = GetRepositoryWithData();
        
        var result = repo.GetCheepsFromAuthor("Name", 1);
        
        Assert.All(result, c => Assert.Equal("Name", c.Author.FirstName));
    }

    [Fact]
    public void CreateCheep_Throws_IfNoAuthor() {
        using var context = CreateInMemoryDatabase();
        var repo = new CheepRepository(context);
        
        
        var cheep = new Cheep {
            Id = 1,
            Text = "This should fail because there is no author",
            TimeStamp = DateTime.UtcNow,
        };

        Assert.ThrowsAsync<NullReferenceException>(() => repo.CreateCheep(cheep.Text, cheep.Author.Email));
    }

    [Fact]
    public void SortByTimeTest() {
	    var repo = new CheepRepository(null); 
	    List<Cheep> list = new List<Cheep>();

	    var author = new Author
	    {
		    Id = 0,
		    FirstName = "John",
		    Email = "test@itu.dk"
	    };

	    var oldCheep = new Cheep
	    {
		    Id = 0,
		    Author = author,
		    AuthorId = 0,
		    Text = "this is the oldest cheep",
		    TimeStamp = DateTime.Now
	    };

	    var newCheep = new Cheep
	    {
		    Id = 1,
		    Author = author,
		    AuthorId = 0,
		    Text = "this is the newest cheep",
		    TimeStamp = DateTime.Now.AddMinutes(10)
	    };
	    
	    list.Add(oldCheep);
	    list.Add(newCheep);
	    
	    //asserts that the list is not sortet correctly yet
		Assert.True(list[0] == oldCheep);
		Assert.True(list[1] == newCheep);
		
		//sort the list so the newest cheeps are at the top of the list
	    repo.SortByTime(list);
	    
	    //assert that the list is now in the correct order
	    Assert.True(list[0] == newCheep);
	    Assert.True(list[1] == oldCheep);
    }

    [Fact]
    public void getCheepsTest() {
	    var repo = GetRepositoryWithData();

	    List<Cheep> list = new List<Cheep>(); //empty list
	    
	    Assert.Empty(list);

	    //use the GetCheeps to add cheeps to the list
	    list = repo.GetCheeps(0);
		
	    //assert the list is no longer empty
	    Assert.NotEmpty(list);
    }

    [Fact]
    public void GetCheepsFromAuthorTets() {
	    using var context = GetContextWithData();
	    var repo = new CheepRepository(context);
	    List<Cheep> list = new List<Cheep>();//empty list
	    

	    list = repo.GetCheepsFromAuthor("John", 0);
	    Assert.NotEmpty(list); //the list should now not be empty
	    Assert.All(list, c => Assert.Equal(c.Author.FirstName, "John")); //Only Johns Cheeps should be in the the list
    }
    
    [Fact]
    public async Task FindAuthorByUserNameTest() {
	    using var context = GetContextWithData();
	    var repo = new CheepRepository(context);
	    
	    var author = await repo.FindAuthorByUserName("Johnyboi");
	    
	    Assert.NotNull(author);
	    Assert.Equal("example1@itu.dk", author.Email);
    }

    [Fact]
    public void CreateAuthorTest() {
	    using var context = GetContextWithData();
	    var repo = new CheepRepository(context);
	    
	    var newAuthor = new Author{
		    FirstName = "Ida",
		    Email = "idaExample@test.dk",
		    UserName = "o3bida"
	    };
		repo.CreateAuthor(newAuthor);

		Assert.Contains(newAuthor, context.Authors.ToList());
    }

    [Fact]
    public void CreateCheepTest() {
	    using var context = GetContextWithData();
	    var repo = new CheepRepository(context);
	    
	    repo.CreateCheep("this is a new cheep!", "johnyboi");
	    
	    Assert.Contains(context.Cheeps, c => c.Text == "this is a new cheep!");
    }

}