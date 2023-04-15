using Microsoft.EntityFrameworkCore;
using TicketHive.Server.Data;
using TicketHive.Shared;

namespace TicketHive.Server.Repos.EventsRepo
{
    public class EventsRepo : IEventsRepo
    {
        private readonly AppDbContext context;

        public EventsRepo(AppDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Adds an event to the database.
        /// </summary>
        /// <param name="eventToAdd">The event to add.</param>
        public async Task AddEventAsync(EventModel eventToAdd)
        {
            context.Events.Add(eventToAdd);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="userToAdd">The user to add.</param>
        public async Task AddUserAsync(UserModel userToAdd)
        {
            context.Users.Add(userToAdd);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds an event to a user's list of events.
        /// </summary>
        /// <param name="username">The username of the user to add the event to.</param>
        /// <param name="eventId">The ID of the event to add.</param>
        /// <returns>True if the event was added successfully, false otherwise.</returns>
        public async Task<bool> AddEventToUserAsync(string username, int eventId)
        {
            var dbEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
            var dbUser = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (dbEvent != null && dbUser != null)
            {
                dbUser.Events.Add(dbEvent);
                try
                {
                    await context.SaveChangesAsync();
                } catch (Exception ex) {
                    Console.WriteLine(ex);
                }
                
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes an event from the database.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>True if the event was deleted successfully, false otherwise.</returns>
        public async Task<bool> DeleteEventAsync(int id)
        {
            var dbEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (dbEvent != null)
            {
                context.Events.Remove(dbEvent);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets all events from the database.
        /// </summary>
        /// <returns>A list of all events in the database.</returns>
        public async Task<List<EventModel>?> GetAllEventsAsync()
        {
            return await context.Events.ToListAsync();
        }

        /// <summary>
        /// Gets all events for a specific user.
        /// </summary>
        /// <param name="username">The username of the user to get events for.</param>
        /// <returns>A list of all events for the specified user.</returns>
        public async Task<List<EventModel>> GetAllEventsFromUserAsync(string username)
        {
            var dbUser = await context.Users.Include(u => u.Events).FirstOrDefaultAsync(u => u.Username == username);
            
            if (dbUser != null )
            {
                return dbUser.Events.ToList();
            }

            return null;
        }

        /// <summary>
        /// Gets an event by its ID.
        /// </summary>
        /// <param name="id">The ID of the event to get.</param>
        /// <returns>The event with the specified ID, or null if it doesn't exist.</returns>
        public async Task<EventModel?> GetEventByIdAsync(int id)
        {
            return await context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Updates the specified event with the provided data.
        /// </summary>
        /// <param name="eventToUpdate">The event model containing the updated data.</param>
        /// <param name="id">The ID of the event to be updated.</param>
        /// <returns>The updated event model, or null if no event was found with the specified ID.</returns>
        public async Task<EventModel> UpdateEventAsync(EventModel eventToUpdate, int id)
        {
            var dbEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (dbEvent != null)
            {
                dbEvent.EventType = eventToUpdate.EventType;
                dbEvent.Name = eventToUpdate.Name;
                dbEvent.Location = eventToUpdate.Location;
                dbEvent.Image = eventToUpdate.Image;
                dbEvent.DateTime = eventToUpdate.DateTime;
                dbEvent.TicketPrice = eventToUpdate.TicketPrice;
                dbEvent.MaxCapacity = eventToUpdate.MaxCapacity;
                dbEvent.IsFullyBooked = eventToUpdate.IsFullyBooked;
                dbEvent.SoldTickets = eventToUpdate.SoldTickets;

                await context.SaveChangesAsync();
                return dbEvent;
            }

            return null;
        }

    }
}
