using Chirp.CLI.Models;

namespace Chirp.CLI;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using SimpleDB;

public class UserInterface
{
    public static void PrintCheeps(IEnumerable<Cheep> cheeps)
    {
        foreach (Cheep cheep in cheeps)
        {
            var time = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(cheep.Timestamp).ToString("MM/dd/yy hh:mm:ss",CultureInfo.InvariantCulture);
            Console.WriteLine(cheep.Author + " @ " + time + ": "+ cheep.Message);
        }
    }
}