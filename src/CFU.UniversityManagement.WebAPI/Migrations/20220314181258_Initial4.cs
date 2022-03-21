using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFU.UniversityManagement.WebAPI.Migrations;

public partial class Initial4 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "buildings",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                BuildingNumber = table.Column<int>(type: "int", nullable: true),
                Block = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Version = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_buildings", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "structure-units",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AuditoriumNumber = table.Column<int>(type: "int", nullable: false),
                BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Version = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_structure-units", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "auditoriums",
            columns: table => new
            {
                Number = table.Column<int>(type: "int", nullable: false),
                BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                StructureUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Capacity = table.Column<int>(type: "int", nullable: true),
                Version = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_auditoriums", x => new { x.BuildingId, x.Number });
                table.ForeignKey(
                    name: "FK_auditoriums_buildings_BuildingId",
                    column: x => x.BuildingId,
                    principalTable: "buildings",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_auditoriums_structure-units_StructureUnitId",
                    column: x => x.StructureUnitId,
                    principalTable: "structure-units",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_auditoriums_StructureUnitId",
            table: "auditoriums",
            column: "StructureUnitId",
            unique: true,
            filter: "[StructureUnitId] IS NOT NULL");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "auditoriums");

        migrationBuilder.DropTable(
            name: "buildings");

        migrationBuilder.DropTable(
            name: "structure-units");
    }
}
