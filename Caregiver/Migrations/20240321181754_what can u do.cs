using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caregiver.Migrations
{
	/// <inheritdoc />
	public partial class whatcanudo : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "WhatCanCaregiverDo",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true);
		}


		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "WhatCanCaregiverDo",
				table: "AspNetUsers");
		}
	}
}
