namespace Chirp.Tests;

using Chirp.CLI;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Chirp.CLI.Program.Main(["--read"]);
    }
}