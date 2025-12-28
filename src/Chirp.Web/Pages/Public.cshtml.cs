using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core1;
using Chirp.Infrastructure.Repositories;


namespace Chirp.Web.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepRepository _repo;
    public List<Cheep> Cheeps { get; set; } = new();
    public Author? CurrentUser { get; private set; }
    public HashSet<int> FollowingList { get; set; } = new();
    
    [BindProperty] public string Message { get; set; }
    
    public PublicModel(ICheepRepository repo)
    {
        _repo = repo;
        
    }
    
    // Jeg tror nok den her metode skal være async fordi getCheeps er det, men ikke sikker vv
    /*public ActionResult OnGet([FromQuery] int page = 1)
    {
        Cheeps =  _repo.GetCheeps(page);
        if (User.Identity?.IsAuthenticated == true)
        {
            CurrentUser = _repo.FindAuthorByEmail(User.Identity.Name ?? "");
        }
        return Page();
    }*/
    
    public async Task<IActionResult> OnGetAsync([FromQuery] int page = 1)
    {
        Cheeps = _repo.GetCheeps(page);
        
        if (User.Identity?.IsAuthenticated == true)
        {
            CurrentUser = await _repo.FindAuthorByEmail(User.Identity.Name ?? "");
        }

        return Page();
    }


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
    public async Task<IActionResult> OnPostFollowAsync(int authorId)
    {
        var me = await _repo.FindAuthorByEmail(User.Identity.Name);
        await _repo.FollowAsync(me!.Id, authorId);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUnfollowAsync(int authorId)
    {
        var me = await _repo.FindAuthorByEmail(User.Identity.Name);
        await _repo.UnfollowAsync(me!.Id, authorId);
        return RedirectToPage();
    }

    
   // public ActionResult OnGet()
    //{
    //  Cheeps = _service.GetCheeps();
    //return Page();
    //}
    

}
