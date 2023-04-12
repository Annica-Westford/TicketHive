using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketHive.Server.Models;
using TicketHive.Server.Repos.UsersRepo;

namespace TicketHive.Server.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUsersRepo repo;

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]

        public string? Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string Message { get; set; } = string.Empty;

        public LoginModel(IUsersRepo repo)
        {
            this.repo = repo;
        }
        public void OnGet()
        {
        }

        /// <summary>
        /// Handles the HTTP POST request for logging in a user with a username and password.
        /// </summary>
        /// <returns>
        /// A task of IAcionResult that represents the result of the action. If the user login
        /// is successful, the action redirects the user to the home page. If the login fails or the
        /// model state is invalid, the action returns the current page with any validation errors displayed.
        /// </returns>
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var signInResult = await repo.SignInUserWithUserName(Username!, Password!, false, false);

                if (signInResult.Succeeded)
                {
                    return Redirect("~/home");
                }
                else
                {
                    Message = "Failed to log in";
                }
            }
            
            return Page();
        }
    }
}
