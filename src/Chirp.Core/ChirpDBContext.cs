namespace Chirp.Core;

public class ChirpDBContext
{
    public List<Author> Authors { get; set; }
    public List<Cheep> Cheeps { get; set; }

    public void SaveChanges() { }
    
}