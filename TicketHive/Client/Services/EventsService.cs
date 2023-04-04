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
        public async Task<bool> AddEventAsync(EventModel eventToAdd)
        {
            var result = await httpClient.PostAsJsonAsync("api/events", eventToAdd);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await httpClient.DeleteAsync($"api/events/{id}");

            if (result.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<List<EventModel>?> GetAllAsync()
        {
            var result = await httpClient.GetFromJsonAsync<List<EventModel>>("api/events");

            if (result != null)
            {
                return result;
            }

            return null;
        }

        public async Task<EventModel?> GetByIdAsync(int id)
        {
            var result = await httpClient.GetFromJsonAsync<EventModel>($"api/events/{id}");

            if (result != null)
            {
                return result;
            }

            return null;
        }

        public async Task<EventModel?> UpdateAsync(EventModel eventToUpdate)
        {
            var result = await httpClient.PutAsJsonAsync($"api/cats/{eventToUpdate.Id}", eventToUpdate);

            if (result.IsSuccessStatusCode)
            {
                EventModel? updatedEvent = await result.Content.ReadFromJsonAsync<EventModel>();
                return updatedEvent;
            }

            return null;
        }
    }
}
