using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketHive.Server.Repos.UsersRepo;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUsersRepo repo;

        public IdentityController(IUsersRepo repo)
        {
            this.repo = repo;
        }

        [HttpPut("{username}/{currentPassword}/{newPassword}")]
        public async Task<IActionResult> ChangePasswordAsync(string username, string currentPassword, string newPassword)
        {
            var result = await repo.ChangePasswordAsync(username, currentPassword, newPassword);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
