using Microsoft.AspNetCore.Identity;
using TicketHive.Server.Models;

namespace TicketHive.Server.Repos.UsersRepo
{
    public class UsersRepo : IUsersRepo
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersRepo(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<SignInResult> SignInUserWithUserName(string username, string password, bool isPersistent, bool lockOutOnFailure)
        {
            return await signInManager.PasswordSignInAsync(username, password, isPersistent, lockOutOnFailure);
        }

        //OBS! Ta bort denna ifall den inte kommer användas
        public async Task<SignInResult> SignInUser(ApplicationUser newUser, string password, bool isPersistent, bool lockOutOnFailure)
        {
            return await signInManager.PasswordSignInAsync(newUser, password, isPersistent, lockOutOnFailure);
        }

        public async Task<IdentityResult> RegisterNewUser(ApplicationUser newUser, string password)
        {
            return await signInManager.UserManager.CreateAsync(newUser, password);
        }
    }
}
