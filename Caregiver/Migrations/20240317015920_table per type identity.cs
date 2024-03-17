using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caregiver.Migrations
{
	/// <inheritdoc />
	public partial class tablepertypeidentity : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<int>(
				name: "YearsOfExperience",
				table: "AspNetUsers",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "PricePerHour",
				table: "AspNetUsers",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "PricePerDay",
				table: "AspNetUsers",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "JobTitle",
				table: "AspNetUsers",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "JobLocationLookingFor",
				table: "AspNetUsers",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "City",
				table: "AspNetUsers",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AlterColumn<int>(
				name: "CareerLevel",
				table: "AspNetUsers",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AddColumn<int>(
				name: "CaregiverId",
				table: "AspNetUsers",
				type: "int",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "Discriminator",
				table: "AspNetUsers",
				type: "nvarchar(13)",
				maxLength: 13,
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<int>(
				name: "PatientId",
				table: "AspNetUsers",
				type: "int",
				nullable: true);

			migrationBuilder.CreateTable(
				name: "Reservations",
				columns: table => new
				{
					CaregiverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Reservations", x => new { x.PatientId, x.CaregiverId });
					table.ForeignKey(
						name: "FK_Reservations_AspNetUsers_CaregiverId",
						column: x => x.CaregiverId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
					table.ForeignKey(
						name: "FK_Reservations_AspNetUsers_PatientId",
						column: x => x.PatientId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Reservations_CaregiverId",
				table: "Reservations",
				column: "CaregiverId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Reservations");

			migrationBuilder.DropColumn(
				name: "CaregiverId",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "Discriminator",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "PatientId",
				table: "AspNetUsers");

			migrationBuilder.AlterColumn<int>(
				name: "YearsOfExperience",
				table: "AspNetUsers",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "PricePerHour",
				table: "AspNetUsers",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "PricePerDay",
				table: "AspNetUsers",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "JobTitle",
				table: "AspNetUsers",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "JobLocationLookingFor",
				table: "AspNetUsers",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "City",
				table: "AspNetUsers",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AlterColumn<int>(
				name: "CareerLevel",
				table: "AspNetUsers",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);
		}
	}
}
