using TicketHive.Shared;

namespace TicketHive.Server.Repos.TicketHiveRepo
{
    public interface ITicketHiveRepo
    {
        Task<List<EventModel>?> GetAllAsync();
        Task<EventModel?> GetByIdAsync(int id);
        Task AddAsync(EventModel eventToAdd);
        Task<EventModel> UpdateAsync(EventModel eventToUpdate, int id);
        Task<bool> DeleteAsync(int id);
    }
}
