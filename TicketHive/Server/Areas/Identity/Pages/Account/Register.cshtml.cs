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
            //Om alla properties �r bindade som de ska
            if (ModelState.IsValid)
            {
                //Skapa en ny ApplicationUser med anv�ndarnamnet som �r inskrivet
                ApplicationUser newUser = new()
                {
                    UserName = Username
                };

                //Testa att registrera anv�ndaren med l�senordet den skrev in 
                var registerResult = await repo.RegisterNewUser(newUser, Password!);

                //Om registreringsf�rs�ker lyckades
                if (registerResult.Succeeded)
                {
                    //skapa en instans av UserModel
                    UserModel newTicketHiveUser = new()
                    {
                        Username = newUser.UserName
                    };

                    //l�gg till den i TicketHive-databasen
                    await eventsRepo.AddUserAsync(newTicketHiveUser);

                    //Testa att logga in anv�ndaren med l�senordet
                    //var signInResult = await signInManager.PasswordSignInAsync(newUser, Password!, false, false);
                    ////Om inloggningsf�rs�ket lyckades
                    //if (signInResult.Succeeded)
                    //{
                    //    //Detta dirigerar anv�ndaren till startsidan, som ju �r en Blazor component
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
