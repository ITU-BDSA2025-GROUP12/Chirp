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
        UserInterface UI = new UserInterface();
            
        if (args[0] == "read")
        {
            UserInterface.PrintCheeps(db.Read());
        }
        else if (args[0] == "cheep")
        {
            db.Store(new Cheep(Environment.UserName, args[1], DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
        }

        
        return 0;
    }
}


    
    