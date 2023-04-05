using System.Text.Json.Serialization;

namespace TicketHive.Shared
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string EventType { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string? Image { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TicketPrice { get; set; }
        public int MaxCapacity { get; set; }
        public int SoldTickets { get; set; }
        public bool IsFullyBooked { get; set; }

        [JsonIgnore]
        public List<UserModel> Users { get; set; } = new();

    }
}
