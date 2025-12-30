using Xunit;
using Chirp.Core1;

namespace Chirp.Core1.Tests;

public class cheepTests {
	[Fact]
	public void cheepHasCorrectValues()
	{
		DateTime timeStamp = DateTime.Now;
		var cheep = new Cheep
		{
			Text = "This is a cheep!",
			TimeStamp = timeStamp,
			AuthorId = 0,
			Author = new Author{FirstName = "Brian"},
			Id = 0	
		};
		Assert.NotNull(cheep);
		Assert.Equal("This is a cheep!", cheep.Text);
		Assert.Equal(timeStamp, cheep.TimeStamp);
		Assert.Equal(0, cheep.Id);
		Assert.Equal("Brian", cheep.Author.FirstName);
		Assert.Equal(0, cheep.Id);
	}
}