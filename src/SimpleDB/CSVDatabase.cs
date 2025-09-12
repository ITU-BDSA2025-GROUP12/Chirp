using Chirp.CLI.Models;
using Microsoft.CSharp.RuntimeBinder;

namespace SimpleDB;

using System;
using System.Collections.Generic;
using System.Globalization;
using CsvHelper;

public sealed class CSVDatabase<T> : IDatabaseRepository<T> {

    //this method reads the CSV file using the external library CsvHelper and prints it in the right format
    public IEnumerable<T> Read(int? limit = null)
    {
        List<T> result = new List<T>();
        using (var reader = new StreamReader("../../assets/chirp_cli_db.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var record = csv.GetRecord<T>();
                result.Add(record);
            }

            return result;
        }
    }
    
    //this method appends a new cheep (int the right format) to a CSV file, using the external library CsvHelper
    public void Store(T record)
    {
        //long time = (long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        //record = new Cheep(Environment.UserName, message, time);
        
        using (var writer = new StreamWriter("../../assets/chirp_cli_db.csv", true))
        using (var csv = new CsvWriter(writer, culture: CultureInfo.InvariantCulture))
        {
            csv.WriteRecord(record);
            csv.NextRecord();
        }
    }
    // converter for epoch time to our time
    public string ConvertUnix(long epoch)
    {
        // epoch counts from 1970 jan 1 at 0 o'clock.
        string dt = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(epoch).ToString("MM/dd/yy h:mm:ss").Replace("-","/");
        // the original format is with . in the time and this replaces . with :
        //return new string(dt.Replace('.', ':'));
        return dt;
    }
    
}
