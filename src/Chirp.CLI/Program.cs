using Chirp.CLI.Models;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.ComponentModel;
using System.Net.Http.Headers;
using System;
using System.CommandLine.Invocation;
using System.Globalization;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace Chirp.CLI;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using SimpleDB;



public class Program
{
    public static int Main (string[] args){
        IDatabaseRepository<Cheep> db = new CSVDatabase<Cheep>();
        UserInterface UI = new UserInterface();
        Option<string> cheepOpt = new Option<string>(
                "--cheep");
        Option<bool> readOpt = new Option<bool>("--read");
        readOpt.DefaultValueFactory = _ => true;
        var rootCommand = new RootCommand();
        rootCommand.Add(cheepOpt);
        rootCommand.Add(readOpt);
        ParseResult parseResult = rootCommand.Parse(args);
        if (parseResult.Errors.Count == 0)
        {
            if (parseResult.GetValue(cheepOpt) is string c)
            {
                db.Store(new Cheep(Environment.UserName, args[1], DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
            }
            else if (parseResult.GetValue(readOpt))
            {
            UserInterface.PrintCheeps(db.Read());
            }
            return 0;
        }
        else
        {
                foreach (ParseError parseError in parseResult.Errors)
                {
                    Console.WriteLine(parseError.Message);
                }
            return 1;
        }
    }
}