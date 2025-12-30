using System.ComponentModel.DataAnnotations;
namespace Chirp.Core1;
/// <summary>
/// Represents a post made by a user
/// </summary>
public class Cheep
{
    /// <summary>
    /// Unique identifier (primary key in database)
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Text inputted by the user
    /// Maximum 160 characters
    /// </summary>
    [MaxLength(160)]
    public required string Text { get; set; }
    /// <summary>
    /// The time this cheep was posted
    /// </summary>
    public required DateTime TimeStamp { get; set; }
    /// <summary>
    /// Uniquely identifies the author of the cheep
    /// (Foreign key in database)
    /// </summary>
    public int AuthorId { get; set; }
    /// <summary>
    /// The author of the cheep
    /// </summary>
    public Author Author { get; set; } = null!;
}
