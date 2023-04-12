using System.Net.Http.Json;
using TicketHive.Shared;

namespace TicketHive.Client.Services
{
    public class EventsService : IEventsService
    {
        private readonly HttpClient httpClient;

        public EventsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Adds a new event to the database.
        /// </summary>
        /// <param name="eventToAdd">The event to add.</param>
        /// <returns>True if the event was added successfully; otherwise, false.</returns>
        public async Task<bool> AddEventAsync(EventModel eventToAdd)
        {
            var result = await httpClient.PostAsJsonAsync("api/events", eventToAdd);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes an event from the database.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>True if the event was deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteEventAsync(int id)
        {
            var result = await httpClient.DeleteAsync($"api/events/{id}");

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retrieves all events from the database.
        /// </summary>
        /// <returns>A list of all events in the database; or null if there are no events.</returns>
        public async Task<List<EventModel>?> GetAllEventsAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<EventModel>>("api/events");

            if (result != null)
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Retrieves a single event from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the event to retrieve.</param>
        /// <returns>The event with the specified ID; or null if no event was found.</returns>
        public async Task<EventModel?> GetEventByIdAsync(int id)
        {
            var result = await httpClient.GetFromJsonAsync<EventModel>($"api/events/event/{id}");

            if (result != null)
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Retrieves all events in the database that were created by the specified user.
        /// </summary>
        /// <param name="username">The username of the user who created the events.</param>
        /// <returns>A list of events created by the specified user; or null if no events were found.</returns>
        public async Task<List<EventModel>?> GetAllEventsFromUserAsync(string username)
        {
            var result = await httpClient.GetFromJsonAsync<List<EventModel>>($"api/events/user/{username}");

            if (result != null)
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Updates an existing event in the database.
        /// </summary>
        /// <param name="eventToUpdate">The event to update.</param>
        /// <returns>The updated event if it was updated successfully; otherwise, null.</returns>
        public async Task<EventModel?> UpdateEventAsync(EventModel eventToUpdate)
        {
            var result = await httpClient.PutAsJsonAsync($"api/events/{eventToUpdate.Id}", eventToUpdate);

            if (result.IsSuccessStatusCode)
            {
                EventModel? updatedEvent = await result.Content.ReadFromJsonAsync<EventModel>();
                return updatedEvent;
            }

            return null;
        }

        /// <summary>
        /// Adds an event to a user in the database.
        /// </summary>
        /// <param name="username">The username of the user to add the event to.</param>
        /// <param name="eventId">The ID of the event to add.</param>
        /// <returns>True if the operation is successful; otherwise, false.</returns>
        public async Task<bool> AddEventToUserAsync(string username, int eventId)
        {
            var result = await httpClient.PutAsync($"api/events/{username}/{eventId}", null);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
