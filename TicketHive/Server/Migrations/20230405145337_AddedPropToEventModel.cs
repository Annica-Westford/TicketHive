using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketHive.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropToEventModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");
        }
    }
}
