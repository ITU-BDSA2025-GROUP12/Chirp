namespace Chirp.CSVDB.Tests;

using System.Threading.Tasks;
using Chirp.CLI;

public class UnitTest1
{
    //TO DO: add integration test checks that a entry can be received from database after it was stored
    //[Fact(Skip = "HTTP integration tests fail in CI")]
    [Fact]
    public async Task TestingIntegration()
    {
        //integrationtest
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        await Chirp.CLI.Program.Main(["--read"]);
        int beforeStore = stringWriter.ToString().Length;
        StringWriter newStringWriter = new StringWriter();
        Console.SetOut(newStringWriter);
        await Chirp.CLI.Program.Main(["--read"]);
        int AfterStore = newStringWriter.ToString().Length;
        // output should be equal after two reads without changes. We cannot change the csv database
        // since the filesystem on azure is read-only.
        Assert.True(beforeStore == AfterStore);

    }
}