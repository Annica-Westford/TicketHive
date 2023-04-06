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
    }
}
