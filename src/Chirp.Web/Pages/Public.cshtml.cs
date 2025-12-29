using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core1;
using Chirp.Infrastructure.Repositories;


namespace Chirp.Web.Pages;

public class PublicModel : PageModel
{
    /// <summary>
    /// CheepRepository to fetch the cheeps to display
    /// </summary>
    private readonly ICheepRepository _repo;
    /// <summary>
    /// List of cheeps to display
    /// </summary>
    public List<Cheep> Cheeps { get; set; } = new();
    /// <summary>
    /// The user who is logged in on this instance of the application
    /// </summary>
    public Author? CurrentUser { get; private set; }
    
    [BindProperty] 
    [StringLength(160, ErrorMessage = "Oops, that's too long!")]
    public string Message { get; set; }
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repo">CheepRepository to fetch the cheeps to display</param>
    public PublicModel(ICheepRepository repo)
    {
        _repo = repo;
    }
    /// <summary>
    /// Gets the current page with cheeps when the app is refreshed
    /// </summary>
    /// <param name="page">Which section of cheeps to request from the repository</param>
    /// <returns>The current page</returns>
    public async Task<IActionResult> OnGetAsync([FromQuery] int page = 1)
    {
        Cheeps = _repo.GetCheeps(page);
        
        if (User.Identity?.IsAuthenticated == true)
        {
            CurrentUser = await _repo.FindAuthorByEmail(User.Identity.Name ?? "");
        }

        return Page();
    }
    /// <summary>
    /// Lets the user post a cheep
    /// </summary>
    /// <returns>Redirect to the main page</returns>
    public async Task<IActionResult> OnPost()
    {
        var userName = User?.Identity?.Name;

        if (string.IsNullOrEmpty(userName))
        {
            // If not logged in -> go to login page
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        await _repo.CreateCheep(Message, userName);
        return RedirectToPage("Public");
    }
    /// <summary>
    /// Lets a user follow an author
    /// </summary>
    /// <param name="authorId">The id of author who will be followed</param>
    /// <returns>Redirects to main page</returns>
    public async Task<IActionResult> OnPostFollowAsync(int authorId)
    {
        var me = await _repo.FindAuthorByEmail(User.Identity.Name);
        await _repo.FollowAsync(me!.Id, authorId);
        return RedirectToPage();
    }
    /// <summary>
    /// Lets a user unfollow an author
    /// </summary>
    /// <param name="authorId">The id of author who will be unfollowed</param>
    /// <returns></returns>
    public async Task<IActionResult> OnPostUnfollowAsync(int authorId)
    {
        var me = await _repo.FindAuthorByEmail(User.Identity.Name);
        await _repo.UnfollowAsync(me!.Id, authorId);
        return RedirectToPage();
    }
    

}
