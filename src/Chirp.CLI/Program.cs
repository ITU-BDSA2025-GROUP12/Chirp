using System.Net.Http.Json;
using System.Net.Http;
using Chirp.Core;
using System.CommandLine;
using System.CommandLine.Parsing;
using System;
using System.Collections.Generic;

namespace Chirp.CLI;

public class Program
{
    public static int Main(string[] args) // Changed BACK to non-async
    {
        Option<string> cheepOpt = new Option<string>("--cheep");
        Option<int> readOpt = new Option<int>("--read");
        readOpt.Arity = ArgumentArity.ZeroOrOne;
        readOpt.DefaultValueFactory = _ => 0;
        
        var rootCommand = new RootCommand();
        rootCommand.Add(cheepOpt);
        rootCommand.Add(readOpt);
        
        ParseResult parseResult = rootCommand.Parse(args);
        
        if (parseResult.Errors.Count == 0)
        {
            using HttpClient client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5172/");
            
            if (parseResult.GetValue(cheepOpt) is string message)
            {
                // POST request to store a cheep (using .Result)
                var newCheep = new Cheep(Environment.UserName, message, ConvertingUnix());
                var response = client.PostAsJsonAsync("cheep", newCheep).Result; // Added .Result
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Cheep posted successfully!");
            }
            else if (parseResult.GetValue(readOpt) is int i)
            {
                // GET request to read cheeps (using .Result)
                var cheeps = client.GetFromJsonAsync<List<Cheep>>("cheeps").Result; // Added .Result
                UserInterface.PrintCheeps(cheeps ?? new List<Cheep>());            }
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
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}