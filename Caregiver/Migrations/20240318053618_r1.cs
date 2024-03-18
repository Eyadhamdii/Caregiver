using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caregiver.Migrations
{
    /// <inheritdoc />
    public partial class r1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaregiverSchedule",
                columns: table => new
                {
                    CaregiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaregiverSchedule", x => x.CaregiverId);
                    table.ForeignKey(
                        name: "FK_CaregiverSchedule_AspNetUsers_CaregiverId",
                        column: x => x.CaregiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaregiverSchedule");
        }
    }
}
