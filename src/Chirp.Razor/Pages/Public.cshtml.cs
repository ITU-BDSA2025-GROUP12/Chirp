using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepViewModel> Cheeps { get; set; }

    public PublicModel(ICheepService service)
    {
        _service = service;
    }
    
//Jeg tror nok den her metode skal være async fordi getCheeps er det, men ikke sikker vv
    public async Task<IActionResult> OnGet([FromQuery] int page = 1)
    {
	    Cheeps = await _service.GetCheeps(page);
	    return Page();
    }
    
   // public ActionResult OnGet()
    //{
    //  Cheeps = _service.GetCheeps();
    //return Page();
    //}
    

}
