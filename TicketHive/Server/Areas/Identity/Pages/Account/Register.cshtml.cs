using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using TicketHive.Server.Models;

namespace TicketHive.Server.Areas.Identity.Pages.Account
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        [Required(ErrorMessage = "Username is required")]
        [MinLength(5, ErrorMessage = "Username must be at least 5 characters")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters")]
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match!")]
        public string VerifiedPassword { get; set; }


        public RegisterModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
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
                var registerResult = await signInManager.UserManager.CreateAsync(newUser, Password!);

                //Om registreringsf�rs�ker lyckades
                if (registerResult.Succeeded)
                {
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
            }

            //Rendera om sidan om modellerna inte var bindade korrekt
            return Page();
        }
    }
}