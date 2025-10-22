namespace Chirp.Razor;
using Microsoft.Data.Sqlite;

public class DBFacade
{
    private readonly string dbpath;
    public DBFacade(string dbpath)
    {
        this.dbpath = dbpath;;
    }
    public List<CheepViewModel> GetCheeps(int page)
    {
        const int pageSize = 32; //32 cheeps per side
        int offset = (page - 1) *  pageSize; //hvor mange cheeps skal springes over, dvs
        //hvis vi er på side to skal den vise cheeps 32 - 64, så springe de første 32 over

        //This is the list we will return
        var cheeps = new List<CheepViewModel>();

        //Create a connection to database
        using var connection = new SqliteConnection(dbpath);
        //Open connection to database
        connection.Open();

        //Creating the sql query
        using var command = connection.CreateCommand();
        command.CommandText = """
                              SELECT u.username, m.text,
                                     datetime(m.pub_date, 'unixepoch') as timestamp
                              FROM message m
                              JOIN user u ON m.author_id = u.user_id
                              ORDER BY m.pub_date DESC
                              LIMIT @limit OFFSET @offset
                              """;
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            string username = reader["username"].ToString();
            string text = reader["text"].ToString();
            string timestamp = reader["timestamp"].ToString();
            cheeps.Add(new CheepViewModel(username, text, timestamp));
        }

        return cheeps;
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author, int page)
    {
        var cheeps = new List<CheepViewModel>();

        const int pageSize = 32; //32 cheeps per side
        int offset = (page - 1) *  pageSize; //hvor mange cheeps skal springes over, dvs
        //hvis vi er på side to skal den vise cheeps 32 - 64, så springe de første 32 over

        using var connection = new SqliteConnection(dbpath);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
                              SELECT u.username, m.text,
                                     datetime(m.pub_date, 'unixepoch') as timestamp
                              FROM message m
                              JOIN user u ON m.author_id = u.user_id
                              WHERE u.username = @username
                              ORDER BY m.pub_date DESC
                              LIMIT @limit OFFSET @offset
                              """;

        command.Parameters.AddWithValue("@username", author);
        command.Parameters.AddWithValue("@limit", pageSize);
        command.Parameters.AddWithValue("@offset", offset);


        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            string username = reader["username"].ToString();
            string text = reader["text"].ToString();
            string timestamp = reader["timestamp"].ToString();
            cheeps.Add(new CheepViewModel(username, text, timestamp));
        }

        return cheeps;
    }

}