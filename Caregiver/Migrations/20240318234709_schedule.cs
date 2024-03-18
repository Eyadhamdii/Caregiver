using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caregiver.Migrations
{
    /// <inheritdoc />
    public partial class schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaregiverSchedule_AspNetUsers_CaregiverId",
                table: "CaregiverSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaregiverSchedule",
                table: "CaregiverSchedule");

            migrationBuilder.RenameTable(
                name: "CaregiverSchedule",
                newName: "CaregiverSchedules");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaregiverSchedules",
                table: "CaregiverSchedules",
                column: "CaregiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaregiverSchedules_AspNetUsers_CaregiverId",
                table: "CaregiverSchedules",
                column: "CaregiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaregiverSchedules_AspNetUsers_CaregiverId",
                table: "CaregiverSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaregiverSchedules",
                table: "CaregiverSchedules");

            migrationBuilder.RenameTable(
                name: "CaregiverSchedules",
                newName: "CaregiverSchedule");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaregiverSchedule",
                table: "CaregiverSchedule",
                column: "CaregiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaregiverSchedule_AspNetUsers_CaregiverId",
                table: "CaregiverSchedule",
                column: "CaregiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
