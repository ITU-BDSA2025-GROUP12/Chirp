using Microsoft.AspNetCore.Identity;

public class Author : IdentityUser<int>{
	public String? FirstName { get; set; } = string.Empty;
	public ICollection<Cheep> Cheeps { get; } = new List<Cheep>();

	public required String Email{ get; set; }
}