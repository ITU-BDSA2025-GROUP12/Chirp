using Microsoft.Data.Sqlite;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public Task<List<CheepViewModel>> GetCheeps();
    public Task<List<CheepViewModel>> GetCheepsFromAuthor(string author);
}

public class CheepService : ICheepService
{
    private readonly DBFacade _dbFacade;

    public CheepService(DBFacade dbFacade)
    {
        _dbFacade = dbFacade;
    }

    public async Task<List<CheepViewModel>> GetCheeps()
    {
        var cheeps = new List<CheepViewModel>();
        
        using var connection = await _dbFacade.CreateConnection();
        var command = new SqliteCommand(
            "SELECT m.text, m.published_at, u.name, u.email " +
            "FROM message m JOIN user u ON m.author_id = u.user_id " +
            "ORDER BY m.published_at DESC LIMIT 32", 
            connection);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            cheeps.Add(new CheepViewModel(
                reader.GetString(2), // author name
                reader.GetString(0), // message text  
                UnixTimeStampToDateTimeString(reader.GetDateTime(1)) // converted timestamp
            ));
        }

        return cheeps;
    }

    public async Task<List<CheepViewModel>> GetCheepsFromAuthor(string author)
    {
        var cheeps = new List<CheepViewModel>();
        
        using var connection = await _dbFacade.CreateConnection();
        var command = new SqliteCommand(
            "SELECT m.text, m.published_at, u.name, u.email " +
            "FROM message m JOIN user u ON m.author_id = u.user_id " +
            "WHERE u.name = @author " +
            "ORDER BY m.published_at DESC LIMIT 32", 
            connection);
            
        command.Parameters.AddWithValue("@author", author);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            cheeps.Add(new CheepViewModel(
                reader.GetString(2), // author name
                reader.GetString(0), // message text  
                UnixTimeStampToDateTimeString(reader.GetDateTime(1)) // converted timestamp
            ));
        }

        return cheeps;
    }

    private static string UnixTimeStampToDateTimeString(DateTime dateTime)
    {
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }
}