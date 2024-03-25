using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caregiver.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPriceWithfees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "totalPriceWithfees",
                table: "Reservations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalPriceWithfees",
                table: "Reservations");
        }
    }
}
