using Chirp.CLI.Models;

namespace Chirp.CLI;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using SimpleDB;


public class Program
{
    static int Main(string[] args)
    {
        IDatabaseRepository<Cheep> db = new CSVDatabase<Cheep>();
            
        if (args[0] == "read")
        {
            foreach (Cheep cheep in db.Read())
            {
                var time = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(cheep.Timestamp).ToString("MM/dd/yy hh:mm:ss",CultureInfo.InvariantCulture);
                Console.WriteLine(cheep.Author + " @ " + time + ": "+ cheep.Message);
            }
        }
        else if (args[0] == "cheep")
        {
            db.Store(new Cheep(Environment.UserName, args[1], DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
        }

        return 0;
    }
}


    
    