public class Author { 
	public int AuthorId { get; set; } 
	public String Name { get; set; } //null??
	public String Email { get; set; } //null??
	public ICollection<Cheep> cheeps { get; set; } //null??
	
	public Cheep Cheep { get; set; }
}