using TicketHive.Shared;

namespace TicketHive.Client.Services
{
    public interface IEventsService
    {
        Task<List<EventModel>?> GetAllAsync();
        Task<EventModel?> GetByIdAsync(int id);
        Task<EventModel?> UpdateAsync(EventModel eventToUpdate);
        Task<bool> AddEventAsync(EventModel eventToAdd);
        Task<bool> DeleteAsync(int id);
    }
}
