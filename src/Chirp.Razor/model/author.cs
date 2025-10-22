public class Author {
	public required int AuthorId { get; set; }
	public required String Name { get; set; }
	public required String Email { get; set; }
	public ICollection<Cheep> Cheeps { get; set; }
}