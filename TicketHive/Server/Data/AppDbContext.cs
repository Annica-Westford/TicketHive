using Microsoft.EntityFrameworkCore;
using TicketHive.Shared;

namespace TicketHive.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<EventModel> Events { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventModel>().HasData(
                new EventModel()
                {
                    Id = 1,
                    Name = "Harry Potter In Concert",
                    EventType = "Concert",
                    Location = "Sweden",
                    DateTime = new DateTime(2023, 12, 23, 20, 00, 00),
                    TicketPrice = 450m,
                    MaxCapacity = 500,
                    IsFullyBooked = false,
                    Image = "harrypotter"
                },
                new EventModel()
                {
                    Id = 2,
                    Name = "BronyCon",
                    EventType = "Convention",
                    Location = "Denmark",
                    DateTime = new DateTime(2024, 03, 10, 09, 00, 00),
                    TicketPrice = 950m,
                    MaxCapacity = 400,
                    IsFullyBooked = false,
                    Image = "bronycon"
                },
                new EventModel()
                {
                    Id = 3,
                    Name = "D&D with Albin",
                    EventType = "Games",
                    Location = "Sweden",
                    DateTime = new DateTime(2023, 05, 01, 08, 00, 00),
                    TicketPrice = 20m,
                    MaxCapacity = 2,
                    IsFullyBooked = false,
                    Image = "dndalbin"
                },
                new EventModel()
                {
                    Id = 4,
                    Name = "Internet Cat Video Festival",
                    EventType = "Festival",
                    Location = "Poland",
                    DateTime = new DateTime(2023, 07, 15, 10, 00, 00),
                    TicketPrice = 300m,
                    MaxCapacity = 700,
                    IsFullyBooked = false,
                    Image = "internetcatfest"
                }
                );
            //base.OnModelCreating(modelBuilder);

            // Seedar user och admin i TicketHive
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel()
                {
                    Id = 1,
                    Username = "user",
                },
                new UserModel()
                {
                    Id = 2,
                    Username = "admin",
                }
                );
        }
    }
}
