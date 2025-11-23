public interface ICheepRepository
{
    public List<Cheep> GetCheeps(int page); // Query
    public List<Cheep> GetCheepsFromAuthor(string author, int page); // Query
    public List<Cheep> GetCheepsFromEmail(string email, int page); // Query
    //Task<int> GetCheepCount();
    Task<int> GetCheepCountFromAuthor(string author);
    Task<string> FindAuthorNameByEmail(string name);
    Task<Author?> FindAuthorByEmail(string email);
    Task CreateAuthor(Author author);
    void CreateCheep(String message, String name);

}