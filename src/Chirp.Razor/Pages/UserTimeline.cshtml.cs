using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepRepository _repo;

    public List<Cheep> Cheeps { get; set; } = new();

    public UserTimelineModel(ICheepRepository repo) => _repo = repo;

    public IActionResult OnGet(string author, [FromQuery] int page = 1)
    {
        if (string.IsNullOrWhiteSpace(author)) return NotFound();
        Cheeps = _repo.GetCheepsFromAuthor(author, page).ToList();
        return Page();
    }

    // private ActionResult OnGet(string author, [FromQuery] int page)
    // {
    //     Cheeps = _service.GetCheepsFromAuthor(author, page);
    //     return Page();
    // }
}
