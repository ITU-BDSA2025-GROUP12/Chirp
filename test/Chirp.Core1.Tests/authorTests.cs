using Xunit;


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
            Id = 0,
            Author = author,
            CheepId = 0
        };
        author.Cheeps.Add(cheep);

        Assert.Equal("John", author.FirstName);
        Assert.NotNull(author.Cheeps);
        Assert.Contains(cheep, author.Cheeps);
    }
    
}