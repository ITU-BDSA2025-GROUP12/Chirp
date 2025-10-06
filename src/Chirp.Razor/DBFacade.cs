using Microsoft.Data.Sqlite;

namespace Chirp.Razor.Services;

public class DBFacade
{
    private readonly string _connectionString;

    public DBFacade(string databasePath)
    {
        _connectionString = $"Data Source={databasePath}";
		InitializeDatabase();
		ImportSampleData();
    }

    public async Task<SqliteConnection> CreateConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }

 	private void InitializeDatabase() {
>
	}
	private void ImportSampleData() {

	}
}