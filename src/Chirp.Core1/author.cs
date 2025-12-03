using Microsoft.AspNetCore.Identity;

public class Author : IdentityUser<int>{
	public String? FirstName { get; set; } = string.Empty;
	public ICollection<Cheep> Cheeps { get; } = new List<Cheep>();

	public HashSet<Author> Following = new HashSet<Author>();

	public void followAuthor(Author author)
	{
		Following.Add(author);
	}

	public void unfollowAuthor(Author author)
	{
		Following.Remove(author);
	}
	
	public bool Followed(Author author)
	{
		return Following.Contains(author);
	}
	
	//public required String Email{ get; set; }
	
	/*
	follow feature plan of action:
	- tilføj følgende til author: 
	- set<author> af followed
	- metode (bool) der tjekker om man følger en author
	- metode der tilføjer til følgelisten
	- 
	- html ændringer:
	- follow button ud fra hvert cheep
	- hvis !following(), display "follow", ellers display "unfollow"
	- (^^fjern/tilføj til sættet accordingly^^)
	
	- inde på userTimeline:
	- hent cheeps fra følgere og display
	- (måske gør så man altid "følger" sig selv)
	 
	 */
}