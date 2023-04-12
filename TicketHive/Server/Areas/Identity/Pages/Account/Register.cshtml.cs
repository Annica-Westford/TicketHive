using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketHive.Server.Models;
using TicketHive.Server.Repos.EventsRepo;
using TicketHive.Server.Repos.UsersRepo;
using TicketHive.Shared;
using TicketHive.Shared.Enums;

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
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$",
    ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string? Password { get; set; }

        [BindProperty]
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match!")]
        public string VerifiedPassword { get; set; }

        [BindProperty]
        [Required]
        //[FromForm(Name = "Country")]
        public string SelectedCountry { get; set; }

        public string Message { get; set; } = string.Empty;

        public RegisterModel(IUsersRepo usersRepo, IEventsRepo eventsRepo)
        {
            this.repo = usersRepo;
            this.eventsRepo = eventsRepo;
        }
        public void OnGet()
        {
        }

        /// <summary>
        /// Handles the HTTP POST request for registering a new user
        /// </summary>
        /// <returns>A Task of IActionResult that represents the result of the action. If the
        /// user registation is successful the action redirects the user to the login page. If it
        /// fails or the model state is invalid, the action returns the current page and displays 
        /// the validation errors
        /// </returns>
        public async Task<IActionResult> OnPost()
        {
            //Om alla properties är bindade som de ska
            if (ModelState.IsValid)
            {
                //Skapa en ny ApplicationUser med användarnamnet som är inskrivet
                ApplicationUser newUser = new()
                {
                    UserName = Username,
                    Country = SelectedCountry
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

                    await eventsRepo.AddUserAsync(newTicketHiveUser);

                    return Redirect("/Authentication/Login");
                }
                else
                {
                    Message = "Failed to Register New User";
                }
            }

            return Page();
        }
    }
}
