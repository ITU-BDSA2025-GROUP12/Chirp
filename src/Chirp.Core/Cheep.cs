namespace Chirp.Core;

public record Cheep
{
    public int CheepId;
    public int AuthorId;
    public string Author;
    public string Text;
    public DateTime Timestamp;
}