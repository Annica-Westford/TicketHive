using TicketHive.Shared;

namespace TicketHive.Server.Repos.EventsRepo
{
    public interface IEventsRepo
    {
        Task<List<EventModel>?> GetAllAsync();
        Task<EventModel?> GetByIdAsync(int id);
        Task AddEventAsync(EventModel eventToAdd);
        Task AddUserAsync(UserModel userToAdd);
        Task<bool> AddEventToUserAsync(string username, int eventId);
        Task<EventModel> UpdateAsync(EventModel eventToUpdate, int id);
        Task<bool> DeleteAsync(int id);
    }
}
