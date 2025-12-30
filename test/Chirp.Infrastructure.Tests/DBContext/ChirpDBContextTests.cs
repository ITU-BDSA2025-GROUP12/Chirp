using Chirp.Infrastructure;
using Chirp.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Chirp.Core1;
using Chirp.Infrastructure.DBContext;

namespace Chirp.Infrastructure.Tests;

public class ChirpDBContextTests {
	public ChirpDBContext CreateInMemoryDatabase(){
		var connection = new SqliteConnection("DataSource=:memory:");
		connection.Open();

		var options = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection).Options;
		var context = new ChirpDBContext(options);
		context.Database.EnsureCreated();
		return context;
	}

	[Fact]
	public void emailIsUnique() {
		using var context = CreateInMemoryDatabase();

		var author1 = new Author
		{
			FirstName = "John",
			Email = "example@itu.dk",
			UserName = "Johnyboi"
		};
		var author2 = new Author
		{
			FirstName = "Annika",
			Email = "example@itu.dk", //not a unique email
			UserName = "Annikatten"
		};
		
		context.Authors.Add(author1);
		context.SaveChanges();
		context.Authors.Add(author2);
		Assert.Throws<DbUpdateException>(() => context.SaveChanges());
	}
}