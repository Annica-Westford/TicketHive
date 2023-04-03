using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketHive.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "DateTime", "EventType", "Image", "IsFullyBooked", "Location", "MaxCapacity", "Name", "TicketPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 23, 20, 0, 0, 0, DateTimeKind.Unspecified), "Concert", null, false, "Sweden", 500, "Harry Potter In Concert", 450m },
                    { 2, new DateTime(2024, 3, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), "Convention", null, false, "Denmark", 400, "BronyCon", 950m },
                    { 3, new DateTime(2023, 5, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "Games", null, false, "Sweden", 2, "D&D with Albin", 20m },
                    { 4, new DateTime(2023, 7, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Festival", null, false, "Poland", 700, "Internet Cat Video Festival", 300m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
