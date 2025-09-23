namespace Chirp.Tests;

using Chirp.CLI;
using Chirp.Core;
using System.Globalization;
using System.Net.Security;

public class UnitTest1
{
    [Fact(Skip = "HTTP calls fail in CI - needs mocking")]

    public void TestingMain()
    {
        //End to end test
        var result = Chirp.CLI.Program.Main(["--read"]);
        Assert.Equal(0, result);
    }


    [Fact(Skip = "HTTP calls fail in CI - needs mocking")]
    public void TestingMainRead()
    {
        //end to end test
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
    [Fact(Skip = "HTTP calls fail in CI - needs mocking")]

    public void TestingMainCheep()
    {
        //end to end test
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        Chirp.CLI.Program.Main(["--cheep", "hello"]);
        Chirp.CLI.Program.Main(["--read"]);
        string actual_output = stringWriter.ToString();
        string expected_output = "annarasmussen @ 09/07/25 12:19:25: hej gutter\n" +
            "Freja @ 09/12/25 08:02:53: hej gunter\n" +
            "ditte @ 09/12/25 03:46:17: hej hej\n" +
            "ditte @ 09/12/25 03:51:41: hej igen\n";
        Cheep cheep = new Cheep(Environment.UserName, "hello", Chirp.CLI.Program.ConvertingUnix());
        var time = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(cheep.Timestamp).ToString("MM/dd/yy hh:mm:ss",CultureInfo.InvariantCulture);
        string addedString = cheep.Author + " @ " + time + ": " + cheep.Message;
        string combinedString = expected_output + addedString + "\n";
        Assert.Equal(combinedString, actual_output);
    }


    [Fact(Skip = "HTTP calls fail in CI - needs mocking")]

    public void TestingUnixConverting()
    {
        //unit test
        long currentUnix = Chirp.CLI.Program.ConvertingUnix();
        string time = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(currentUnix).ToString("MM/dd/yy hh:mm:ss", CultureInfo.InvariantCulture);
        int lengthOfString = time.Length;
        int expectedLength = 17;
        Boolean correctSyntax = false;
        if (time[2] == '/' && time[5] == '/' && time[11] == ':'  && time[14] == ':')
        {
            correctSyntax = true;
        }
        Assert.Equal(expectedLength, lengthOfString);
        Assert.True(correctSyntax);
    }
}