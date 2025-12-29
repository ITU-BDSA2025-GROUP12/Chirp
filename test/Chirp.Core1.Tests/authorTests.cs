using Xunit;
using Chirp.Core1;


namespace Chirp.Core1.Tests;

public class UnitTest1
{ 
    [Fact]
    public void authorHasCorrectvalues()
    {
        DateTime timeStamp = DateTime.Now;

        var author = new Author
        {
            FirstName = "John"
        };
        var cheep = new Cheep
        {
            Text = "I love Chirp!",
            TimeStamp = timeStamp,
            AuthorId = 0,
            Author = author,
            Id = 0
        };
        author.Cheeps.Add(cheep);

        Assert.Equal("John", author.FirstName);
        Assert.NotNull(author.Cheeps);
        Assert.Contains(cheep, author.Cheeps);
    }
    
}