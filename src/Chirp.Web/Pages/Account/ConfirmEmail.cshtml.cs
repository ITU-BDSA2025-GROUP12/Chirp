// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Chirp.Core1;

namespace Chirp.Web.Pages.Account{
    public class ConfirmEmailModel : PageModel
    {
        /// <summary>
        /// Keeps track of current user
        /// </summary>
        private readonly UserManager<Author> _userManager;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager">Keeps track of current user</param>
        public ConfirmEmailModel(UserManager<Author> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }
        /// <summary>
        /// Verifies the email of the current user
        /// </summary>
        /// <param name="userId">Id of the author whose email will be verified</param>
        /// <param name="code">Encoded email</param>
        /// <returns>Current page</returns>
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Public");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }
    }
}
