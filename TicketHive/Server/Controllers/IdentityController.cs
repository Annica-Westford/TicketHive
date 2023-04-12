using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketHive.Server.Repos.UsersRepo;
using TicketHive.Shared;

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

        /// <summary>
        /// Changes the password of the user with the specified username, given the current password and the new password.
        /// </summary>
        /// <param name="username">The username of the user whose password should be changed.</param>
        /// <param name="currentPassword">The user's current password.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <returns>An IActionResult representing the result of the password change operation.</returns>
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

        /// <summary>
        /// Updates the country of the user with the given username to the specified new country.
        /// </summary>
        /// <param name="username">The username of the user to update.</param>
        /// <param name="newCountry">The new country to set for the user.</param>
        /// <returns>An IActionResult representing the result of the update operation.</returns>
        [HttpPut("{username}/{newCountry}")]
        public async Task<IActionResult> UpdateUserCountryAsync(string username, string newCountry)
        {
            bool hasUpdatedCountrySuccesfully = await repo.UpdateUserCountryAsync(username, newCountry);

            if (hasUpdatedCountrySuccesfully)
            {
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Retrieves the country of the user with the given username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve the country for.</param>
        /// <returns>An ActionResult of string representing the result of the operation.</returns>
        [HttpGet("{username}")]
        public async Task<ActionResult<string?>> GetCountryByIdAsync(string username)
        {
            string? userCountry = await repo.GetUserCountryAsync(username);

            if (userCountry != null)
            {
                return Ok(userCountry);
            }

            return NotFound();
        }
    }
}
