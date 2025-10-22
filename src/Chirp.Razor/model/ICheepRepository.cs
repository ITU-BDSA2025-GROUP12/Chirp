public interface ICheepRepository
{
    public List<Cheep> GetCheeps(int page);
    public List<Cheep> GetCheepsFromAuthor(string author, int page);
}