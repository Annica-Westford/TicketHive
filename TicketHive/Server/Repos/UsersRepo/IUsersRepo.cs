using Microsoft.AspNetCore.Identity;
using TicketHive.Server.Models;

namespace TicketHive.Server.Repos.UsersRepo
{
    public interface IUsersRepo
    {
        Task<SignInResult> SignInUserWithUserName(string username, string password, bool isPersistent, bool lockOutOnFailure);
        Task<IdentityResult> RegisterNewUser(ApplicationUser newUser, string password);
        Task<IdentityResult> ChangePasswordAsync(string username, string currentPassword, string newPassword);
        Task<bool> UpdateUserCountryAsync(string username, string newCountry);
        Task<string?> GetUserCountryAsync(string username);

    }
}
