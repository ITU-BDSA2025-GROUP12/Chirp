using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity; 
namespace Chirp.Core1;

public class Cheep
{
    public int Id { get; set; }   // Primary key

    [MaxLength(160)]
    public required string Text { get; set; }

    public required DateTime TimeStamp { get; set; }

    public int AuthorId { get; set; }   // Foreign key
    public Author Author { get; set; } = null!;
}
