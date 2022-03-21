using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFU.UniversityManagement.WebAPI.Migrations;

public partial class Initial9 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "Address_BuildingId",
            table: "buildings",
            type: "uniqueidentifier",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Address_BuildingId",
            table: "buildings");
    }
}
