namespace Chirp.Core;

public record Cheep
{
    public required int CheepId;
    public required int AuthorId;
    public required Author Author;
    public required string Text;
    public required DateTime TimeStamp;
}