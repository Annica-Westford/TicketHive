namespace TicketHive.Client.Services
{
    public interface IIdentityService
    {
        Task<bool> ChangePasswordAsync(string username, string currentPassword, string newPassword);

    }
}
