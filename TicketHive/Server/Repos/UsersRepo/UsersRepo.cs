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

        /// <summary>
        /// Signs in a user with their username and password
        /// </summary>
        /// <param name="username">Username of the user to sign in</param>
        /// <param name="password">Password of the user to sign in</param>
        /// <param name="isPersistent">Whether or not the user's session should persist</param>
        /// <param name="lockOutOnFailure">Whether or not to lock out the user after a certain number of failed attempts</param>
        /// <returns>SignInResult of the sign in attempt</returns>
        public async Task<SignInResult> SignInUserWithUserName(string username, string password, bool isPersistent, bool lockOutOnFailure)
        {
            return await signInManager.PasswordSignInAsync(username, password, isPersistent, lockOutOnFailure);
        }

        /// <summary>
        /// Registers a new user with the given details
        /// </summary>
        /// <param name="newUser">ApplicationUser instance with the details of the new user</param>
        /// <param name="password">Password of the new user</param>
        /// <returns>IdentityResult of the registration attempt</returns>
        public async Task<IdentityResult> RegisterNewUser(ApplicationUser newUser, string password)
        {
            return await signInManager.UserManager.CreateAsync(newUser, password);
        }

        /// <summary>
        /// Changes the password of the user with the given username
        /// </summary>
        /// <param name="username">Username of the user to change the password for</param>
        /// <param name="currentPassword">Current password of the user</param>
        /// <param name="newPassword">New password for the user</param>
        /// <returns>IdentityResult of the password change attempt</returns>
        public async Task<IdentityResult> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var user = await signInManager.UserManager.FindByNameAsync(username);

            if (user != null)
            {
                return await signInManager.UserManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }

            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        /// <summary>
        /// Retrieves the country of the user with the given username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>The country of the user, or null if the user was not found.</returns>
        public async Task<string?> GetUserCountryAsync(string username)
        {
            var user = await signInManager.UserManager.FindByNameAsync(username);

            if (user != null)
            {
                return user.Country;
            }

            return null;
        }

        /// <summary>
        /// Updates the country of the user with the given username to the specified new country.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="newCountry">The new country to set for the user.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public async Task<bool> UpdateUserCountryAsync(string username, string newCountry)
        {
            var user = await signInManager.UserManager.FindByNameAsync(username);

            if (user != null)
            {
                user.Country = newCountry;

                var result = await signInManager.UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    //refresh the user's authentication cookie with the updated user information
                    await signInManager.RefreshSignInAsync(user);
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
