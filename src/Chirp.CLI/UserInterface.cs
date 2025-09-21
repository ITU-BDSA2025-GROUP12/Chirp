using Chirp.Core;  

namespace Chirp.CLI;

public static class UserInterface
{
    public static void PrintCheeps(IEnumerable<Cheep> cheeps)  // ← CHANGED FROM SimpleDB.Cheep
    {
        foreach (var cheep in cheeps)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp);
            string formattedDate = dateTimeOffset.ToString("MM/dd/yy HH:mm:ss");
            Console.WriteLine($"{cheep.Author} @ {formattedDate}: {cheep.Message}");
        }
    }
}