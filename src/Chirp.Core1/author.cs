using System.ComponentModel.DataAnnotations;
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
	[MaxLength(100)]
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
	[MaxLength(100)]
	public string? DisplayName { get; set; }
	/// <summary>
	/// A collection of authors, this user follows
	/// </summary>
	public ICollection<Author>? Following { get; set; }
	/// <summary>
	/// A collection of users that follow this author
	/// </summary>
	public ICollection<Author>? Followers { get; set; }
	/// <summary>
	/// Checks whether this user follows a given author
	/// </summary>
	/// <param name="authorId">Id of the author who may or may not be followed</param>
	/// <returns>true or false, depending on whether the author is followed</returns>
	public bool IsFollowing(int authorId)
		=> Following != null && Following.Any(a => a.Id == authorId);
	//public required String Email{ get; set; }
}