using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketHive.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    SoldTickets = table.Column<int>(type: "int", nullable: false),
                    IsFullyBooked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventModelUserModel",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventModelUserModel", x => new { x.EventsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_EventModelUserModel_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventModelUserModel_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "DateTime", "Description", "EventType", "Image", "IsFullyBooked", "Location", "MaxCapacity", "Name", "SoldTickets", "TicketPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 23, 20, 0, 0, 0, DateTimeKind.Unspecified), "The Harry Potter Film Concert Series is a unique and immersive experience that combines the magic of film with the enchanting power of live music. \n\nThe series features the beloved Harry Potter films projected on a giant screen while a live symphony orchestra performs the unforgettable score composed by the legendary John Williams and later by Patrick Doyle, Nicholas Hooper, and Alexandre Desplat. Audiences of all ages can relive the adventures of Harry, Ron, and Hermione in a whole new way, as the live music adds a new dimension to the beloved story.\n\nThe Harry Potter Film Concert Series is a must-see for any Harry Potter fan and a testament to the enduring power of the Wizarding World.", "Concert", "harrypotter", false, "Sweden", 500, "Harry Potter In Concert", 287, 450m },
                    { 2, new DateTime(2024, 3, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), "BronyCon was an annual fan convention held on the East Coast of the United States for fans of My Little Pony: Friendship Is Magic, among them adult and teenage bronies.\n\nEleven events were held, with the final one in August 2019 drawing in 10,215 attendees. Though originally planned to run through 2025 it was announced at the closing ceremonies of the 2018 convention that 2019 would be the final year, tying in with the final season of the show.\n\nBronyCon was previously styled as BroNYCon, as its first three conventions were held in New York City. It dropped the 'NYC' capitalization for its fourth event, which was held in nearby Secaucus, New Jersey. The 2013 event marked the convention's move to Baltimore, Maryland, as well as a shift from twice a year to once a year.", "Convention", "bronycon", false, "Denmark", 400, "BronyCon", 17, 950m },
                    { 3, new DateTime(2023, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "Meet up with the local talent Albin for adventures and fun! In a world with dungeons and dragons explore new territory, clash with dark monsters, spin lore and commit heroic feats while playing the greatest RPG of all time!\n\nIn Dungeons and Dragons your group works together on adventures. A group will have a mixture of different types of characters with different strengths and weaknesses. As a team, you decide how you’re going to approach an encounter to make the most of each character’s skills and abilities and support each other.", "Games", "dndalbin", true, "Sweden", 5, "D&D with Albin", 5, 20m },
                    { 4, new DateTime(2023, 7, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "The Internet Cat Video Festival was a national competition that celebrates cat videos on the internet. Many of these festivals include appearances by special guests and celebricats (such as Grumpy Cat and the creator of Nyan Cat), live music, costume contests, art projects, and booths hosting local animal resource nonprofits. The 2013 Minneapolis show featured a cat sculpture made out of butter.\n\nThe fourth and last Film Festival took place in 2015. Walker Art Center discontinued the event \"to put our resources towards the remodeling of the campus including the Minneapolis Sculpture Garden,” according to spokeswoman Rachel Joyce. All of the festival memorabilia was given to the Minnesota Historical Society.", "Festival", "internetcatfest", false, "Poland", 700, "Internet Cat Video Festival", 159, 300m },
                    { 5, new DateTime(2023, 3, 17, 19, 0, 0, 0, DateTimeKind.Unspecified), "Flottkalaset (Raft Festival) is an annual music festival and cultural event held in Tjautjas in the Gällivare Municipality region in northern Sweden. The event was envisioned and organized by Riksspelman Daniel Wikslund, a noted Swedish folk musician.\n\nThe two-day festival takes place in August on an island in the middle of a mountain lake, accessible via rafts or by walking across a raft bridge.", "Festival", "flottkalaset", false, "Sweden", 400, "Flottkalaset", 89, 150m },
                    { 6, new DateTime(2023, 8, 30, 11, 0, 0, 0, DateTimeKind.Unspecified), "World’s biggest food fight. Thousands of people make the pilgrimage to the ancient town of Buñol at the end of August every year to party and pelt each other with tomatoes. Trucks carrying 40 tonnes of tomatoes rock up in the centre of town. Get ready for the undisputed king of food fight festivals.", "Festival", "latomatina", false, "Spain", 3000, "La Tomatina", 2439, 200m },
                    { 7, new DateTime(2023, 8, 13, 13, 0, 0, 0, DateTimeKind.Unspecified), "The Guca Trumpet Festival is a celebration of trumpet culture in Serbian folk music, an instrument that has deep historical and political ties to the country. The event has been around since 1961, and today it is considered by many to be among the world’s best folk and music festivals.\n\nUpon first glance, the festival may not seem that much different from other music festivals. There are tented pop-up bars and stalls selling food, but it’s injected with a whole lot of authentic Serbian elements: whole roasted animals are on display, inviting carnivores to eat to their heart’s content. Get drunk on the music and rakija as you revel in the folk dances where performers are decked out in bright costumes entertaining an energetic audience.", "Festival", "gucatrumpet", false, "Serbia", 2500, "Guca Trumpet Festival", 570, 350m },
                    { 8, new DateTime(2023, 7, 4, 19, 0, 0, 0, DateTimeKind.Unspecified), "The Nathan's Famous International Hot Dog Eating Contest is an annual American hot dog competitive eating competition. It is held each year on July 4th at Nathan's Famous Corporation's original, and best-known restaurant at the corner of Surf and Stillwell Avenues in Coney Island, a neighborhood of Brooklyn, New York City.\n\nThe contest has gained public attention in recent years due to the stardom of Takeru \"The Tsunami\" Kobayashi and Joey \"Jaws\" Chestnut. The defending men's champion is Chestnut, who ate 63 hot dogs in the 2022 contest. The defending women's champion is Miki Sudo, who ate 40 hot dogs in 2022.", "Competition", "nathanshotdog", false, "USA", 1500, "Nathan's Hot Dog Eating Contest", 264, 50m },
                    { 9, new DateTime(2023, 3, 24, 17, 0, 0, 0, DateTimeKind.Unspecified), "Everybody’s favorite Drag Queens are back on Friday March 24 for a command performance in our gorgeous winery ballroom.\n\nIt’ll be a night of Burlesque, Bites and Booze with this super fun and unforgettable 21+ Drag Show Cocktail Party. Admission is $65/pp. This includes the show and free Hors D’oeuvres during the 2 hour cocktail hour before. (Cash bar will be open all evening).", "Party", "dragqueen", false, "USA", 75, "Drag Queen Burlesque", 69, 650m },
                    { 10, new DateTime(2023, 4, 7, 16, 0, 0, 0, DateTimeKind.Unspecified), "DreamHack is an ESL Gaming brand specializing in esports tournaments and other gaming conventions. It is recognized by the Guinness Book of Records and Twin Galaxies as being the world's largest LAN party and computer festival with the world's fastest Internet connection. It usually holds its events in Western Europe and North America.\n\nDreamHack's events include local area network gatherings with live concerts and competitions in digital art and esports. The first event was held in Malung, Sweden. The company has held several gaming events throughout Europe, in Stockholm, Jönköping, Tours, Bucharest, Cluj, Valencia, Seville, London, and Leipzig. In May 2016, it held its first North American event in Austin, Texas. In August 2016, it held its first Canada event in Montreal, Quebec. Winter events consistently have about 10% more visitors than summer events.", "Games", "dreamhack", false, "Sweden", 750, "DreamHack", 630, 890m },
                    { 11, new DateTime(2023, 7, 16, 20, 0, 0, 0, DateTimeKind.Unspecified), "Joddla Med Siv is a Swedish comedy-folk-rock band from Hässleholm in Skåne, south Sweden. They formed in February 1990, and today the band has 8 records to be proud about. They have sold about 35 000 records just in the region of Skåne.", "Concert", "joddlamedsiv", false, "Sweden", 30, "Joddla Med Siv", 11, 29m },
                    { 12, new DateTime(2023, 2, 28, 19, 15, 0, 0, DateTimeKind.Unspecified), "Trazan & Banarne was a Swedish children's television series which was broadcast from 1976 to 1982 on Sveriges Television. For its first season from 1976 to 1977, it was part of the Jullovsmorgon series. The title characters were played by Lasse Åberg (Trazan Apansson) and Klasse Möllberg (Banarne), and they recorded many songs together with the band Electric Banana Band.", "Concert", "trazanbanarne", false, "Sweden", 50, "Trazan & Banarne", 48, 3799m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username" },
                values: new object[,]
                {
                    { 1, "user" },
                    { 2, "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventModelUserModel_UsersId",
                table: "EventModelUserModel",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventModelUserModel");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
