namespace Chirp.Tests;

using Chirp.CLI;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var result = Chirp.CLI.Program.Main(["--read"]);
        Assert.Equal(0, result);
    }
}