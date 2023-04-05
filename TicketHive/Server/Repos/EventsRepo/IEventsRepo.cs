using TicketHive.Shared;

namespace TicketHive.Server.Repos.EventsRepo
{
    public interface IEventsRepo
    {
        Task<List<EventModel>?> GetAllEventsAsync();
        Task<EventModel?> GetEventByIdAsync(int id);
        Task<List<EventModel>> GetAllEventsFromUserAsync(string username);
        Task AddEventAsync(EventModel eventToAdd);
        Task AddUserAsync(UserModel userToAdd);
        Task<bool> AddEventToUserAsync(string username, int eventId);
        Task<EventModel> UpdateEventAsync(EventModel eventToUpdate, int id);
        Task<bool> DeleteEventAsync(int id);
    }
}
