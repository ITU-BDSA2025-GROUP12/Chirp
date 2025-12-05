using Microsoft.AspNetCore.Identity;

namespace Chirp.Core1;

public class Author : IdentityUser<int>{
	public String? FirstName { get; set; } = string.Empty;
	public ICollection<Cheep> Cheeps { get; } = new List<Cheep>();
	public bool IsDeleted { get; set; }
	public string? DisplayName { get; set; }

	//public required String Email{ get; set; }
}