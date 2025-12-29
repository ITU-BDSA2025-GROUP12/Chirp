using Microsoft.AspNetCore.Identity;

namespace Chirp.Core1;
/// <summary>
/// Represents a registered user in Chirp!
/// Implements .NET's IdentityUser
/// </summary>
public class Author : IdentityUser<int>{
	/// <summary>
	/// Username of the user
	/// </summary>
	public String? FirstName { get; set; } = string.Empty;
	/// <summary>
	/// A list of every cheep this user has cheep'ed
	/// </summary>
	public ICollection<Cheep> Cheeps { get; } = new List<Cheep>();
	/// <summary>
	/// Returns whether the user is deleted
	/// </summary>
	public bool IsDeleted { get; set; }
	/// <summary>
	/// The name that appears to other users
	/// </summary>
	public string? DisplayName { get; set; }
}