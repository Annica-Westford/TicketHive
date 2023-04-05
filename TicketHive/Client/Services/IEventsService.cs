using TicketHive.Shared;

namespace TicketHive.Client.Services
{
    public interface IEventsService
    {
        Task<List<EventModel>?> GetAllEventsAsync();
        Task<EventModel?> GetEventByIdAsync(int id);
        Task<List<EventModel>?> GetAllEventsFromUserAsync(string username);
        Task<EventModel?> UpdateEventAsync(EventModel eventToUpdate);
        Task<bool> AddEventAsync(EventModel eventToAdd);
        Task<bool> AddEventToUserAsync(string username, int eventId);
        Task<bool> DeleteEventAsync(int id);
    }
}
