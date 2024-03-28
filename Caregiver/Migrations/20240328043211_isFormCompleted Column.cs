using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caregiver.Migrations
{
    /// <inheritdoc />
    public partial class isFormCompletedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFormCompleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFormCompleted",
                table: "AspNetUsers");
        }
    }
}
