using TicketHive.Shared;

namespace TicketHive.Client.Services
{
    public interface IEventsService
    {
        Task<List<EventModel>?> GetAllAsync();
        Task<EventModel?> GetEventByIdAsync(int id);
        Task<EventModel?> UpdateAsync(EventModel eventToUpdate);
        Task<bool> AddEventAsync(EventModel eventToAdd);
        Task<bool> AddEventToUserAsync(string username, int eventId);
        Task<bool> DeleteAsync(int id);
    }
}
