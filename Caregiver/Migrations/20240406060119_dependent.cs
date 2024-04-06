using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caregiver.Migrations
{
    /// <inheritdoc />
    public partial class dependent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationNo",
                table: "Dependants");

            migrationBuilder.AddColumn<int>(
                name: "DependentId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "CaregiverId",
                table: "ReservationDates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DependentId",
                table: "Reservations",
                column: "DependentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDates_CaregiverId",
                table: "ReservationDates",
                column: "CaregiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationDates_AspNetUsers_CaregiverId",
                table: "ReservationDates",
                column: "CaregiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Dependants_DependentId",
                table: "Reservations",
                column: "DependentId",
                principalTable: "Dependants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationDates_AspNetUsers_CaregiverId",
                table: "ReservationDates");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Dependants_DependentId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_DependentId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_ReservationDates_CaregiverId",
                table: "ReservationDates");

            migrationBuilder.DropColumn(
                name: "DependentId",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "CaregiverId",
                table: "ReservationDates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationNo",
                table: "Dependants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
