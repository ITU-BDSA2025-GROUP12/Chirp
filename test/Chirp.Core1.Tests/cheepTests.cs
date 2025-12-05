using Xunit;

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
			Id = 0,
			Author = new Author{FirstName = "Brian"},
			CheepId = 0	
		};
		Assert.NotNull(cheep);
		Assert.Equal("This is a cheep!", cheep.Text);
		Assert.Equal(timeStamp, cheep.TimeStamp);
		Assert.Equal(0, cheep.Id);
		Assert.Equal("Brian", cheep.Author.FirstName);
		Assert.Equal(0, cheep.Id);
	}
}