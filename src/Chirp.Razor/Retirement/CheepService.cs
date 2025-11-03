/* using Chirp.Razor;
using Chirp.Razor.Pages;
using Microsoft.Data.Sqlite;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepDTO> GetCheeps(int page);
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int page);
}

public class CheepService : ICheepService
{
    
    //Dependency injection?
    private readonly DBFacade dbf;
    public CheepService(DBFacade dbf)
    {
        this.dbf = dbf;
    }
    
    //public List<CheepViewModel> GetCheeps(int page)
   // { return dbf.GetCheeps(page); }

   public List<CheepDTO> GetCheeps(int page) //man kan skrive (int page = 1) for at sætte en default men idk om det er nødvendigt
   {
       var cheeps = dbf.GetCheeps(page);
       var cheepDTOs = cheeps.Select( c=> new CheepDTO
           {
               Author = c.Author,
               Text = c.Message, 
               Time = c.Timestamp
           }).ToList();
       
           return cheepDTOs;
   }
   
   //den her skal også lige ændres til at returnere dtos
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int page)
    {
        return dbf.GetCheepsFromAuthor(author, page);
    }

    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }
    
    
}
*/