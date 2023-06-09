﻿namespace TicketHive.Client.Services
{
    public interface IIdentityService
    {
        Task<bool> ChangePasswordAsync(string username, string currentPassword, string newPassword);
        Task<bool> UpdateUserCountryAsync(string username, string newCountry);
        Task<string?> GetUserCountryAsync(string username);
    }
}
