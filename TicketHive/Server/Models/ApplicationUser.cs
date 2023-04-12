using Microsoft.AspNetCore.Identity;
using TicketHive.Shared.Enums;

namespace TicketHive.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Country { get; set; } 
    }
}