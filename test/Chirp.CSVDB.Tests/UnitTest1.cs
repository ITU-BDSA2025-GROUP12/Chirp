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
        await Chirp.CLI.Program.Main(["--cheep",  "hello"]);
        StringWriter newStringWriter = new StringWriter();
        Console.SetOut(newStringWriter);
        await Chirp.CLI.Program.Main(["--read"]);
        int AfterStore = newStringWriter.ToString().Length;
        Boolean changed = false;
        if (beforeStore < AfterStore)
        {
            changed = true;
        }
        Assert.True(changed);

    }
}