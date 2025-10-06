using Microsoft.Data.Sqlite;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public Task<List<CheepViewModel>> GetCheeps(int page);
    public Task<List<CheepViewModel>> GetCheepsFromAuthor(string author);
}

public class CheepService : ICheepService
{
    private readonly DBFacade _dbFacade;

    public CheepService(DBFacade dbFacade)
    {
        _dbFacade = dbFacade;
    }

    public async Task<List<CheepViewModel>> GetCheeps(int page)
    {
        const int pageSize = 32; //32 cheeps per side
        int offset = (page - 1) *  pageSize; //hvor mange cheeps skal springes over, dvs
        //hvis vi er på side to skal den vise cheeps 32 - 64, så springe de første 32 over

        var cheeps = new List<CheepViewModel>();
        
        using var connection = await _dbFacade.CreateConnection();
        var command = new SqliteCommand(
            "SELECT m.text, m.published_at, u.name, u.email " +
            "FROM message m JOIN user u ON m.author_id = u.user_id " +
            "ORDER BY m.published_at DESC " +
            "LIMIT @limit OFFSET @offset",
            connection);
        
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);
        
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