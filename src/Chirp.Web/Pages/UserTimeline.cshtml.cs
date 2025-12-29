using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Infrastructure.Repositories;
using Chirp.Core1;

namespace Chirp.Web.Pages;

public class UserTimelineModel : PageModel
{
    /// <summary>
    /// CheepRepository to fetch the cheeps to display
    /// </summary>
    private readonly ICheepRepository _repo;
    /// <summary>
    /// A list of cheeps to display
    /// </summary>
    public List<Cheep> Cheeps { get; set; } = new();
    /// <summary>
    /// The user who is logged in on this instance of the application
    /// </summary>
    public Author? CurrentUser { get; set; }
    [BindProperty] public string Message { get; set; }
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repo">CheepRepository to fetch the cheeps to display</param>
    public UserTimelineModel(ICheepRepository repo)
    {
        _repo = repo;
    }
    /// <summary>
    /// Gets the current page with cheeps when the app is refreshed
    /// </summary>
    /// <param name="page">Which section of cheeps to request from the repository</param>
    /// <returns>The current page</returns>
    public IActionResult OnGet(string author, [FromQuery] int page = 1)
    {
        if (string.IsNullOrWhiteSpace(author))
            return NotFound();

        if (!User.Identity.IsAuthenticated)
        {
            Cheeps = _repo.GetCheepsFromAuthor(author, page).ToList();
            return Page();
        }
        
        CurrentUser = _repo.FindAuthorByEmail(User.Identity.Name!).Result;
        
        if (CurrentUser == null)
        {
            Cheeps = _repo.GetCheepsFromAuthor(author, page).ToList();
            return Page();
        }
        
        if (CurrentUser.UserName == author || CurrentUser.FirstName == author)
        {
            Cheeps = _repo.GetTimelineCheeps(CurrentUser, page).Result;
        }
        else
        {
            Cheeps = _repo.GetCheepsFromAuthor(author, page).ToList();
        }

        return Page();
    }
    /// <summary>
    /// Lets the user post a cheep
    /// </summary>
    /// <returns>Redirect to user timeline page</returns>
   public async Task<IActionResult> OnPost(string author)
    {
        await _repo.CreateCheep(Message, User.Identity?.Name);
        return RedirectToPage("UserTimeline", new { author });
    }
    /// <summary>
    /// Lets a user unfollow an author
    /// </summary>
    /// <param name="authorId">The id of author who will be unfollowed</param>
    /// <returns></returns>
    public async Task<IActionResult> OnPostFollowAsync(int authorId)
    {
        var me = await _repo.FindAuthorByEmail(User.Identity!.Name!);
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
        var me = await _repo.FindAuthorByEmail(User.Identity!.Name!);
        await _repo.UnfollowAsync(me!.Id, authorId);
        return RedirectToPage();
    }
    
}
