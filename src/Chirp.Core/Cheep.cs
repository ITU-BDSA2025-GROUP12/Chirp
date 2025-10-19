using System.ComponentModel.DataAnnotations;

namespace Chirp.Core;

public class Cheep
{
    public required int CheepId;
    public required int AuthorId;
    public required Author Author;
    public required DateTime TimeStamp;
    
    [MaxLength(160)]
    public required string Text;
    
    
}