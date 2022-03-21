using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFU.UniversityManagement.WebAPI.Migrations;

public partial class Initial1 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "academies",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IsDisbanded = table.Column<bool>(type: "bit", nullable: false),
                Version = table.Column<long>(type: "bigint", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_academies", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "faculties",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                AcademyId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Version = table.Column<long>(type: "bigint", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_faculties", x => x.Id);
                table.ForeignKey(
                    name: "FK_faculties_academies_AcademyId1",
                    column: x => x.AcademyId1,
                    principalTable: "academies",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_faculties_AcademyId1",
            table: "faculties",
            column: "AcademyId1");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "faculties");

        migrationBuilder.DropTable(
            name: "academies");
    }
}
