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

// See https://aka.ms/new-console-template for more information
class Program {


    public class Cheep()
{
    [Index(0)] //'Author' is a index 0 i the CSV file
    public String Author {get; set;}
    
    [Index(1)]//'Message' is a index 1 i the CSV file
    public String Message {get; set;}
    
    [Index(2)] //'Timestamp' is a index 2 i the CSV file
    public int Timestamp {get; set;}
}
    static void Main(String[] args)
    {
        string path = "chirp_cli_db.csv";
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
                cheep(path, c);
            }
            else if (parseResult.GetValue(readOpt))
            {
                read(path);
            } 
                    }
        else
        {
            foreach (ParseError parseError in parseResult.Errors)
            {
                Console.WriteLine(parseError.Message);
            }
        }


        //this method reads the CSV file using the external library CsvHelper and prints it in the right format
        static void read(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = csv.GetRecord<Cheep>();
                    var time = convertUnix(record.Timestamp);
                    Console.WriteLine(record.Author +  " @ " + time + ": " + record.Message);
                }
            }
        }
        //this method appends a new cheep (int the right format) to a CSV file, using the external library CsvHelper
        static void cheep(string path, string message)
        {
            
            int time = (int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            var record = new Cheep{Author = Environment.UserName, Message = message, Timestamp = time};
            
            using (var writer = new StreamWriter(path, true))
            using (var csv = new CsvWriter(writer, culture: CultureInfo.InvariantCulture))
            {
                csv.WriteRecord(record);
                csv.NextRecord();
            }
        }
        // converter for epoch time to our time
        static string convertUnix(int epoch)
        {
            // epoch counts from 1970 jan 1 at 0 o'clock.
            string dt = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(epoch).ToString();
            // the original format is with . in the time and this replaces . with :
            return new string(dt.Replace('.', ':'));
        }

    }

}