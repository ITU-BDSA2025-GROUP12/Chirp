using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using Chirp.Razor;
using Chirp.Core;
using Chirp.Razor.Data; 

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepRepository _repo;

    public List<Cheep> Cheeps { get; set; } = new();

    public PublicModel(ICheepRepository repo)
    {
        _repo = repo;
    }
    
    // Jeg tror nok den her metode skal være async fordi getCheeps er det, men ikke sikker vv
    public async Task<IActionResult> OnGet([FromQuery] int page = 1)
    {
        Cheeps = (await _repo.GetCheeps(page)).ToList();
        return Page();
    }

    // public ActionResult OnGet()
    // {
    //   Cheeps = _service.GetCheeps();
    //   return Page();
    // }
}
