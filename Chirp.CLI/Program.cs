using System;
using System.Text.RegularExpressions;

// converter for epoch time to our time
string convertUnix(int epoch)
{
    // epoch counts from 1970 jan 1 at 0 o'clock.
    string dt = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(epoch).ToString();
    // the original format is with . in the time and this replaces . with :
    return new string(dt.Replace('.', ':'));
}

// hvis man skriver dotnet run -- cheep kalder man kommandoen
if (args[0] == "cheep")
{
    // streamwriter for adding to the text file.
    using (StreamWriter streamWriter = File.AppendText("chirp_cli_db.csv"))
    {
        string username = Environment.UserName;
        // args[1] is everything that is written in the " " part, even with spaces etc.
        string message = args[1];
        // getting the current time
        int time = (int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        // formatting to fit with the other data
        streamWriter.WriteLine(username + ",\"" + message + "\"," + time.ToString());
    }
    
    // hvis man skriver dotnet run -- read kører man kommandoen
} else if (args[0] == "read")
{
    using (StreamReader sr = new("chirp_cli_db.csv"))
    {
        string line;
        while((line = sr.ReadLine()) != null)
        {
            // first we define the regex pattern
            Regex pattern = new Regex("^([A-Za-z]{4,5}),\"([A-Za-z\\s*\\D]+)\",(\\d+)$");
        
            // if the match is a success we take each group and print it out (to test)
            Match match = pattern.Match(line);
            if (match.Success)
            {
                // first group is the username, second is comment etc.
                string username = match.Groups[1].Value;
                string comment = match.Groups[2].Value;
                string timestamp = convertUnix(int.Parse(match.Groups[3].Value));
                // formatting the print out
                Console.WriteLine(username + " @ " + timestamp + ": " + comment);
            }

        }
    }
}
