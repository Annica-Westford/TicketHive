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
        public async Task<bool> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var result = await httpClient.PutAsync($"api/identity/{username}/{currentPassword}/{newPassword}", null);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateUserCountryAsync(string username, string newCountry)
        {
            var result = await httpClient.PutAsync($"api/identity/{username}/{newCountry}", null);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

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
