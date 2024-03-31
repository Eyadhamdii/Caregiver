using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caregiver.Migrations
{
    /// <inheritdoc />
    public partial class AddCaregiverId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaregiverId",
                table: "ReservationDates",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaregiverId",
                table: "ReservationDates");
        }
    }
}
