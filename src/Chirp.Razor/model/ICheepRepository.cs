public interface ICheepRepository
{
    public List<Cheep> GetCheeps(int page); // Query
    public List<Cheep> GetCheepsFromAuthor(string author, int page); // Query
    Task<int> GetCheepCount();
    Task<int> GetCheepCountFromAuthor(string author);
    Task<Author?> FindAuthorByName(string name);
    Task<Author?> FindAuthorByEmail(string email);
    Task CreateAuthor(Author author);
    Task CreateCheep(Cheep cheep);

}