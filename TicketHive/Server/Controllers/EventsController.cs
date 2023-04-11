using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketHive.Server.Repos.EventsRepo;
using TicketHive.Shared;

namespace TicketHive.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsRepo repo;

        public EventsController(IEventsRepo repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Returns a list of all events
        /// </summary>
        /// <returns>List of EventModel instances</returns>
        [HttpGet]
        public async Task<ActionResult<List<EventModel>?>> GetAllEventsAsync()
        {
            var events = await repo.GetAllEventsAsync();
            if(events != null)
            {
                return Ok(events);
            }

            return NotFound();
        }

        /// <summary>
        /// Returns the event with the given id
        /// </summary>
        /// <param name="id">Id of the event to retrieve</param>
        /// <returns>EventModel instance</returns>
        [HttpGet("event/{id}")]
        public async Task<ActionResult<EventModel?>> GetEventByIdAsync(int id)
        {
            var selectedEvent = await repo.GetEventByIdAsync(id);

            if (selectedEvent != null)
            {
                return Ok(selectedEvent);
            }

            return NotFound();
        }

        /// <summary>
        /// Returns a list of all events created by the user with the given username
        /// </summary>
        /// <param name="username">Username of the user to retrieve events for</param>
        /// <returns>List of EventModel instances</returns>
        [HttpGet("user/{username}")]
        public async Task<ActionResult<List<EventModel>?>> GetAllEventsFromUserAsync(string username)
        {
            var userEventsList = await repo.GetAllEventsFromUserAsync(username);

            if (userEventsList != null)
            {
                return Ok(userEventsList);
            }

            return NotFound();
        }

        /// <summary>
        /// Updates the event with the given id using the provided EventModel instance
        /// </summary>
        /// <param name="eventToUpdate">EventModel instance with updated data</param>
        /// <param name="id">Id of the event to update</param>
        /// <returns>Updated EventModel instance</returns>
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<EventModel?>> UpdateEventAsync([FromBody] EventModel eventToUpdate, int id)
        {
            var updatedEvent = await repo.UpdateEventAsync(eventToUpdate, id);
            if (updatedEvent != null)
            {
                return Ok(updatedEvent);
            }

            return NotFound();
        }

        /// <summary>
        /// Adds the event with the given id to the user with the given username
        /// </summary>
        /// <param name="username">Username of the user to add the event to</param>
        /// <param name="eventId">Id of the event to add</param>
        /// <returns>ActionResult</returns>
        [HttpPut("{username}/{eventId}")]
        public async Task<IActionResult> AddEventToUserAsync(string username, int eventId)
        {
            bool isUpdatedSuccessfully = await repo.AddEventToUserAsync(username, eventId);

            if (isUpdatedSuccessfully)
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Adds the given EventModel instance to the repository
        /// </summary>
        /// <param name="eventToAdd">EventModel instance to add</param>
        [HttpPost]
        public async Task<IActionResult> AddEventAsync([FromBody] EventModel eventToAdd)
        {
            await repo.AddEventAsync(eventToAdd);
            return Ok();
        }

        /// <summary>
        /// Deletes the event with the given id from the repository
        /// </summary>
        /// <param name="id">Id of the event to delete</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventAsync(int id)
        {
            bool isDeletedSuccessfully = await repo.DeleteEventAsync(id);
            if (isDeletedSuccessfully)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
