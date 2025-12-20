using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity; 
namespace Chirp.Core1;


public class Cheep  {
	[MaxLength(160)]
	public required String Text { get; set; }

	public required DateTime TimeStamp { get; set; }

	public int Id { get; set; }
	public Author Author { get; set; } = null!;

	public required int CheepId { get; set; }

}