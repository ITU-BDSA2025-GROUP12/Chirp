using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepRepository _repo;

    public List<Cheep> Cheeps { get; set; } = new();

    public PublicModel(ICheepRepository repo)
    {
        _repo = repo;
    }
    
    // Jeg tror nok den her metode skal være async fordi getCheeps er det, men ikke sikker vv
    public IActionResult OnGet([FromQuery] int page = 1)
    {
        Cheeps = _repo.GetCheeps(page);
        return Page();
    }
    
   // public ActionResult OnGet()
    //{
    //  Cheeps = _service.GetCheeps();
    //return Page();
    //}
    

}
