using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caregiver.Migrations
{
    /// <inheritdoc />
    public partial class schedule2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaregiverSchedules_AspNetUsers_CaregiverId",
                table: "CaregiverSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaregiverSchedules",
                table: "CaregiverSchedules");

            migrationBuilder.AlterColumn<string>(
                name: "CaregiverId",
                table: "CaregiverSchedules",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CaregiverSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaregiverSchedules",
                table: "CaregiverSchedules",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CaregiverSchedules_CaregiverId",
                table: "CaregiverSchedules",
                column: "CaregiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaregiverSchedules_AspNetUsers_CaregiverId",
                table: "CaregiverSchedules",
                column: "CaregiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
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

            migrationBuilder.DropIndex(
                name: "IX_CaregiverSchedules_CaregiverId",
                table: "CaregiverSchedules");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CaregiverSchedules");

            migrationBuilder.AlterColumn<string>(
                name: "CaregiverId",
                table: "CaregiverSchedules",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
    }
}
