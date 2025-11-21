using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepRepository _repo;

    public List<Cheep> Cheeps { get; set; } = new();
    
    [BindProperty] public string Message { get; set; }


    public UserTimelineModel(ICheepRepository repo) => _repo = repo;

    public IActionResult OnGet(string author, [FromQuery] int page = 1)
    {
        if (string.IsNullOrWhiteSpace(author)) return NotFound();
        Cheeps = _repo.GetCheepsFromAuthor(author, page).ToList();
        return Page();
    }
    
    public ActionResult OnPost()
    {
        _repo.CreateCheep(Message, User.Identity.Name);
        return RedirectToPage("UserTimeline");
    }
}
