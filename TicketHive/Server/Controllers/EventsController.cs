using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketHive.Server.Repos.EventsRepo;
using TicketHive.Shared;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsRepo repo;

        public EventsController(IEventsRepo repo)
        {
            this.repo = repo;
        }

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

        [HttpPut("{username}/{eventId}")]
        public async Task<IActionResult> AddEventToUserAsync(string username, int eventId)
        {
            bool isUpdatedSuccessfully = await repo.AddEventToUserAsync(username, eventId);

            if (isUpdatedSuccessfully)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddEventAsync([FromBody] EventModel eventToAdd)
        {
            await repo.AddEventAsync(eventToAdd);
            return Ok();
        }


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
