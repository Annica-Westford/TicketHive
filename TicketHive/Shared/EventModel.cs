using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TicketHive.Shared
{
    public class EventModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Required(ErrorMessage = "Event Type is required")]
        public string EventType { get; set; } = null!;

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; } = null!;
        public string? Image { get; set; }

        //[Required(ErrorMessage = "Date and time is required")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "TicketPrice is required")]
        [Range(0, double.MaxValue, ErrorMessage = "TicketPrice must be a positive number")]
        public decimal TicketPrice { get; set; }

        [Required(ErrorMessage = "Max attendees is required")]
        [Range(1, int.MaxValue, ErrorMessage = "MaxCapacity must be a positive number")]
        public int MaxCapacity { get; set; }
        public int SoldTickets { get; set; }
        public bool IsFullyBooked { get; set; }

        [JsonIgnore]
        public List<UserModel> Users { get; set; } = new();

    }
}
