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
        public async Task<ActionResult<List<EventModel>?>> GetAllAsync()
        {
            var events = await repo.GetAllAsync();
            if(events != null)
            {
                return Ok(events);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventModel?>> GetByIdAsync(int id)
        {
            var selectedEvent = await repo.GetByIdAsync(id);

            if (selectedEvent != null)
            {
                return Ok(selectedEvent);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EventModel?>> UpdateAsync([FromBody] EventModel eventToUpdate, int id)
        {
            var updatedEvent = await repo.UpdateAsync(eventToUpdate, id);
            if (updatedEvent != null)
            {
                return Ok(updatedEvent);
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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            bool isDeletedSuccessfully = await repo.DeleteAsync(id);
            if (isDeletedSuccessfully)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
