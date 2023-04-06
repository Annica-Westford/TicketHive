using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketHive.Server.Models;
using TicketHive.Server.Repos.EventsRepo;
using TicketHive.Server.Repos.UsersRepo;
using TicketHive.Shared;

namespace TicketHive.Server.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUsersRepo repo;
        private readonly IEventsRepo eventsRepo;

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Username must be at least 5 characters")]
        public string? Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters")]
        public string? Password { get; set; }

        [BindProperty]
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match!")]
        public string VerifiedPassword { get; set; }

        public string Message { get; set; } = string.Empty;

        public RegisterModel(IUsersRepo usersRepo, IEventsRepo eventsRepo)
        {
            this.repo = usersRepo;
            this.eventsRepo = eventsRepo;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            //Om alla properties är bindade som de ska
            if (ModelState.IsValid)
            {
                //Skapa en ny ApplicationUser med användarnamnet som är inskrivet
                ApplicationUser newUser = new()
                {
                    UserName = Username
                };

                //Testa att registrera användaren med lösenordet den skrev in 
                var registerResult = await repo.RegisterNewUser(newUser, Password!);

                //Om registreringsförsöker lyckades
                if (registerResult.Succeeded)
                {
                    //skapa en instans av UserModel
                    UserModel newTicketHiveUser = new()
                    {
                        Username = newUser.UserName
                    };

                    //lägg till den i TicketHive-databasen
                    await eventsRepo.AddUserAsync(newTicketHiveUser);

                    //Testa att logga in användaren med lösenordet
                    //var signInResult = await signInManager.PasswordSignInAsync(newUser, Password!, false, false);
                    ////Om inloggningsförsöket lyckades
                    //if (signInResult.Succeeded)
                    //{
                    //    //Detta dirigerar användaren till startsidan, som ju är en Blazor component
                    //    return Redirect("~/");
                    //}

                    //Skicka personen till Login-sidan
                    return Redirect("/Authentication/Login");
                }
                else
                {
                    Message = "Failed to Register New User";
                }
            }

            //Rendera om sidan om modellerna inte var bindade korrekt
            return Page();
        }
    }
}
