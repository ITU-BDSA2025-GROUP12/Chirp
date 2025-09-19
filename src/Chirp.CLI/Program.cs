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
    public static int Main(string[] args)
    {
        UserInterface UI = new UserInterface();
        Option<string> cheepOpt = new Option<string>(
                "--cheep");
        Option<int> readOpt = new Option<int>("--read");
        readOpt.Arity = ArgumentArity.ZeroOrOne;
        readOpt.DefaultValueFactory = _ => 0;
        var rootCommand = new RootCommand();
        rootCommand.Add(cheepOpt);
        rootCommand.Add(readOpt);
        ParseResult parseResult = rootCommand.Parse(args);
        if (parseResult.Errors.Count == 0)
        {
            if (parseResult.GetValue(cheepOpt) is string c)
            {
                CSVDatabase<Cheep>.Instance().Store(new Cheep(Environment.UserName, args[1], ConvertingUnix()));
            }
            else if (parseResult.GetValue(readOpt) is int i)
            {
                UserInterface.PrintCheeps(CSVDatabase<Cheep>.Instance().Read(i));
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

    public static long ConvertingUnix()
    {
        long result =  DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return result;
    }


}