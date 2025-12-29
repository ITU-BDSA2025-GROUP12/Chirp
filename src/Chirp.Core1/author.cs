using Microsoft.AspNetCore.Identity;

namespace Chirp.Core1;
/// <summary>
/// Represents a registered user in Chirp!
/// Implements .NET's IdentityUser
/// </summary>
public class Author : IdentityUser<int>{
	/// <summary>
	/// Name of the user
	/// </summary>
	public String? FirstName { get; set; } = string.Empty;
	/// <summary>
	/// A list of every cheep 
	/// </summary>
	public ICollection<Cheep> Cheeps { get; } = new List<Cheep>();
	public bool IsDeleted { get; set; }
	public string? DisplayName { get; set; }

	//public required String Email{ get; set; }
}