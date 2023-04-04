using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketHive.Server.Repos.TicketHiveRepo;

namespace TicketHive.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ITicketHiveRepo repo;

        public EventsController(ITicketHiveRepo repo)
        {
            this.repo = repo;
        }
    }
}
