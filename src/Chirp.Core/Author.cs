namespace  Chirp.Core;

public class Author
{
    public required int AuthorId { get; set; }
    public string Name { get; set; }
    public List<Cheep> Cheeps { get; set; }
    public string Email {get; set;}
    
}
