﻿using Microsoft.EntityFrameworkCore;
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

        public async Task AddEventAsync(EventModel eventToAdd)
        {
            context.Events.Add(eventToAdd);
            await context.SaveChangesAsync();
        }

        public async Task AddUserAsync(UserModel userToAdd)
        {
            context.Users.Add(userToAdd);
            await context.SaveChangesAsync();
        }

        public async Task<bool> AddEventToUserAsync(string username, int eventId)
        {
            var dbEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
            var dbUser = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (dbEvent != null && dbUser != null)
            {
                dbUser.Events.Add(dbEvent);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

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

        public async Task<List<EventModel>?> GetAllEventsAsync()
        {
            return await context.Events.ToListAsync();
        }

        public async Task<List<EventModel>> GetAllEventsFromUserAsync(string username)
        {
            var dbUser = await context.Users.Include(u => u.Events).FirstOrDefaultAsync(u => u.Username == username);
            
            if (dbUser != null )
            {
                return dbUser.Events.ToList();
            }

            return null;
        }

        public async Task<EventModel?> GetEventByIdAsync(int id)
        {
            return await context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateSoldTickets(EventModel eventToUpdate, int id, int ticketUpdate)
        {
            var dbEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (dbEvent != null)
            {
                dbEvent.SoldTickets += ticketUpdate;
                await context.SaveChangesAsync();
                
            }
        }

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