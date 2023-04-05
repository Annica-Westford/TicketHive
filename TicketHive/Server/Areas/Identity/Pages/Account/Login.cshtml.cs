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

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                var signInResult = await repo.SignInUserWithUserName(Username!, Password!, false, false);

                if (signInResult.Succeeded)
                {
                    //lägg in länk till vart man ska skickas när man loggat in!
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
