class Author { 
	public int authorId { get; set; } 
	public String name { get; set; } //null??
	public String email { get; set; } //null??
	public ICollection<Cheep> cheeps { get; set; } //null??
}