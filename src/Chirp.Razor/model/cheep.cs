using System.ComponentModel.DataAnnotations;

public class Cheep {
	[MaxLength(160)]
	public required String Text { get; set; }

	public required DateTime TimeStamp { get; set; }

	public int AuthorId { get; set; }
	public Author Author { get; set; }

	public required int CheepId { get; set; }

}