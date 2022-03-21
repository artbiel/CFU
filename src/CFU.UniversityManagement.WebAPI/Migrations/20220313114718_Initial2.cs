using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFU.UniversityManagement.WebAPI.Migrations;

public partial class Initial2 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_faculties_academies_AcademyId1",
            table: "faculties");

        migrationBuilder.DropIndex(
            name: "IX_faculties_AcademyId1",
            table: "faculties");

        migrationBuilder.DropColumn(
            name: "AcademyId1",
            table: "faculties");

        migrationBuilder.AddColumn<Guid>(
            name: "AcademyId",
            table: "faculties",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.CreateTable(
            name: "institutes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IsDisbanded = table.Column<bool>(type: "bit", nullable: false),
                Version = table.Column<long>(type: "bigint", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_institutes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "departments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FacultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Version = table.Column<long>(type: "bigint", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_departments", x => x.Id);
                table.ForeignKey(
                    name: "FK_departments_faculties_FacultyId",
                    column: x => x.FacultyId,
                    principalTable: "faculties",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_departments_institutes_InstituteId",
                    column: x => x.InstituteId,
                    principalTable: "institutes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_faculties_AcademyId",
            table: "faculties",
            column: "AcademyId");

        migrationBuilder.CreateIndex(
            name: "IX_departments_FacultyId",
            table: "departments",
            column: "FacultyId");

        migrationBuilder.CreateIndex(
            name: "IX_departments_InstituteId",
            table: "departments",
            column: "InstituteId");

        migrationBuilder.AddForeignKey(
            name: "FK_faculties_academies_AcademyId",
            table: "faculties",
            column: "AcademyId",
            principalTable: "academies",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_faculties_academies_AcademyId",
            table: "faculties");

        migrationBuilder.DropTable(
            name: "departments");

        migrationBuilder.DropTable(
            name: "institutes");

        migrationBuilder.DropIndex(
            name: "IX_faculties_AcademyId",
            table: "faculties");

        migrationBuilder.DropColumn(
            name: "AcademyId",
            table: "faculties");

        migrationBuilder.AddColumn<Guid>(
            name: "AcademyId1",
            table: "faculties",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_faculties_AcademyId1",
            table: "faculties",
            column: "AcademyId1");

        migrationBuilder.AddForeignKey(
            name: "FK_faculties_academies_AcademyId1",
            table: "faculties",
            column: "AcademyId1",
            principalTable: "academies",
            principalColumn: "Id");
    }
}
