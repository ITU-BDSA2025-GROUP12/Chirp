// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Chirp.Infrastructure.Data;


namespace newAppp.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<Author> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;
        
        private readonly ICheepRepository _repo;

        public PersonalDataModel(
            UserManager<Author> userManager,
            ILogger<PersonalDataModel> logger,
            ICheepRepository repo)
        {
            _userManager = userManager;
            _logger = logger;
            _repo = repo;;
        }
        
        public String Message { get; set; }
        public string UserName {get; set;}
        public string Email {get; set;}
        public ICollection<Cheep> Cheeps { get; set; } = new List<Cheep>();
        
        
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            Cheeps = _repo.GetCheepsFromAuthor(user.FirstName, 1).ToList();
            
            UserName = userName;
            Email = await _userManager.GetEmailAsync(user);
            Message = ("Hello " + UserName + ", this is your personal data!");
            

            return Page();
        }
    }
}
