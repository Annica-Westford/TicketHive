using Microsoft.AspNetCore.Identity;
using TicketHive.Server.Models;

namespace TicketHive.Server.Repos.UsersRepo
{
    public interface IUsersRepo
    {
        Task<SignInResult> SignInUserWithUserName(string username, string password, bool isPersistent, bool lockOutOnFailure);
        Task<IdentityResult> RegisterNewUser(ApplicationUser newUser, string password);
        Task<SignInResult> SignInUser(ApplicationUser newUser, string password, bool isPersistent, bool lockOutOnFailure);
    }
}
