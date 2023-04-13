using System.Net.Http.Json;
using TicketHive.Shared;

namespace TicketHive.Client.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient httpClient;

        public IdentityService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Changes the password of the user with the given username.
        /// </summary>
        /// <param name="username">The username of the user to change the password for.</param>
        /// <param name="currentPassword">The current password of the user.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <returns>True if the operation was successful and false otherwise.</returns>
        public async Task<bool> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var result = await httpClient.PutAsync($"api/identity/{username}/{currentPassword}/{newPassword}", null);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the country of the user with the given username to the specified new country.
        /// </summary>
        /// <param name="username">The username of the user to update.</param>
        /// <param name="newCountry">The new country to set for the user.</param>
        /// <returns>True if the operation was successful and false otherwise.</returns>
        public async Task<bool> UpdateUserCountryAsync(string username, string newCountry)
        {
            var result = await httpClient.PutAsync($"api/identity/{username}/{newCountry}", null);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retrieves the country of the user with the given username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve the country for.</param>
        /// <returns>A string representing the country of the user, or null if the user does not exist or has no country set.</returns>
        public async Task<string?> GetUserCountryAsync(string username)
        {
            var result = await httpClient.GetStringAsync($"/api/identity/{username}");

            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }

            return null;
        }
    }
}
