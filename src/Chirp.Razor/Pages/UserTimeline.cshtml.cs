using System.Linq;
using System.Threading.Tasks;
using Chirp.Core;
using Chirp.Razor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepRepository _repo;

    public List<Cheep> Cheeps { get; private set; } = new();

    public UserTimelineModel(ICheepRepository repo) => _repo = repo;

    public async Task<IActionResult> OnGet(string author, [FromQuery] int page = 1)
    {
        if (string.IsNullOrWhiteSpace(author)) return NotFound();
        Cheeps = (await _repo.GetCheepsFromAuthor(author, page)).ToList();
    public ActionResult OnGet(string author, [FromQuery] int page)
    {
        Cheeps = _service.GetCheepsFromAuthor(author, page);
        return Page();
    }
}
