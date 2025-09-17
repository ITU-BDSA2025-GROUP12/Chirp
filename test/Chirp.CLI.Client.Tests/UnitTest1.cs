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

    [Fact]
    public void Test2()
    {
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        Chirp.CLI.Program.Main(["--read"]);
        string actual_output = stringWriter.ToString();
        string expected_output = "annarasmussen @ 09/07/25 12:19:25: hej gutter\n" +
            "Freja @ 09/12/25 08:02:53: hej gunter\n" +
            "ditte @ 09/12/25 03:46:17: hej hej\n" +
            "ditte @ 09/12/25 03:51:41: hej igen\n";
        Assert.Equal(expected_output, actual_output);
    }
}