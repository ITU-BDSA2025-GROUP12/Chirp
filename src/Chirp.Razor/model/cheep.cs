public class Cheep {
	public String Text { get; set; }
	
	public DateTime TimeStamp { get; set; }
	
	public int AuthorId { get; set; }

	public int CheepId { get; set; }
	
	public Author Author { get; set; }
}