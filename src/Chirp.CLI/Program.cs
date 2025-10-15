using System.Net.Http.Json;
using System.Net.Http;
using Chirp.Core;
using System.CommandLine;
using System.CommandLine.Parsing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chirp.CLI;

public class Program
{
    public static async Task<int> Main(string[] args) // Changed BACK to non-async
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

            client.BaseAddress = new Uri("https://bdsagroup12chirpremotedb2.azurewebsites.net");
            if (parseResult.GetValue(cheepOpt) is string message)
            {
                // POST request to store a cheep (using .Result)
                var newCheep = new Cheep(Environment.UserName, message, ConvertingUnix());
                var response = await client.PostAsJsonAsync("cheep", newCheep);
                if (response.IsSuccessStatusCode) {
                    Console.WriteLine("Cheep posted successfully!");
                } else {
                    Console.WriteLine("Error, unable to contact server");
                }
            }
            else if (parseResult.GetValue(readOpt) is int i)
            {
                var res = await client.GetAsync("/cheeps");
                if (res.IsSuccessStatusCode) {
                    var cheeps = await res.Content.ReadFromJsonAsync<List<Cheep>>();
                    UserInterface.PrintCheeps(cheeps ?? new List<Cheep>());
                }
                else {
                    Console.WriteLine("Error, unable to contact server");
                }
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
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}