using TicketHive.Server.Data;
using TicketHive.Shared;

namespace TicketHive.Server.Repos.TicketHiveRepo
{
    public class TicketHiveRepo : ITicketHiveRepo
    {
        private readonly AppDbContext context;

        public TicketHiveRepo(AppDbContext context)
        {
            this.context = context;
        }

        public Task AddAsync(EventModel eventToAdd)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EventModel>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EventModel?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EventModel> UpdateAsync(EventModel eventToUpdate, int id)
        {
            throw new NotImplementedException();
        }
    }
}
