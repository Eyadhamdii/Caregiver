using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caregiver.Migrations
{
    /// <inheritdoc />
    public partial class AddFees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "totalPriceWithfees",
                table: "Reservations",
                newName: "TotalPriceWithfees");

            migrationBuilder.RenameColumn(
                name: "totalPrice",
                table: "Reservations",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<double>(
                name: "Fees",
                table: "Reservations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fees",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "TotalPriceWithfees",
                table: "Reservations",
                newName: "totalPriceWithfees");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Reservations",
                newName: "totalPrice");
        }
    }
}
