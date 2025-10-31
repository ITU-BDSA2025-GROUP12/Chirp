public interface ICheepRepository
{
    public List<Cheep> GetCheeps(int page); // Query
    public List<Cheep> GetCheepsFromAuthor(string author, int page); // Query
}