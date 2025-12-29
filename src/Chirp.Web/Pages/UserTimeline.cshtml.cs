using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Infrastructure.Repositories;
using Chirp.Core1;

namespace Chirp.Web.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepRepository _repo;

    public List<Cheep> Cheeps { get; set; } = new();
    public Author? CurrentUser { get; set; }
    [BindProperty] public string Message { get; set; }


    public UserTimelineModel(ICheepRepository repo)
    {
        _repo = repo;
    }
    
    /*public IActionResult OnGet(string author, [FromQuery] int page = 1)
    {
        if (string.IsNullOrWhiteSpace(author)) return NotFound();
        Cheeps = _repo.GetCheepsFromAuthor(author, page).ToList();
        return Page();
    }*/
    
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

    
   public async Task<IActionResult> OnPost(string author)
    {
        await _repo.CreateCheep(Message, User.Identity?.Name);
        return RedirectToPage("UserTimeline", new { author });
    }
   
    public async Task<IActionResult> OnPostFollowAsync(int authorId)
    {
        var me = await _repo.FindAuthorByEmail(User.Identity!.Name!);
        await _repo.FollowAsync(me!.Id, authorId);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUnfollowAsync(int authorId)
    {
        var me = await _repo.FindAuthorByEmail(User.Identity!.Name!);
        await _repo.UnfollowAsync(me!.Id, authorId);
        return RedirectToPage();
    }
    
}
