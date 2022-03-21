using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFU.UniversityManagement.WebAPI.Migrations;

public partial class Initial3 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_departments_faculties_FacultyId",
            table: "departments");

        migrationBuilder.DropForeignKey(
            name: "FK_departments_institutes_InstituteId",
            table: "departments");

        migrationBuilder.DropPrimaryKey(
            name: "PK_departments",
            table: "departments");

        migrationBuilder.DropIndex(
            name: "IX_departments_InstituteId",
            table: "departments");

        migrationBuilder.DropColumn(
            name: "InstituteId",
            table: "departments");

        migrationBuilder.RenameTable(
            name: "departments",
            newName: "faculty-departments");

        migrationBuilder.RenameIndex(
            name: "IX_departments_FacultyId",
            table: "faculty-departments",
            newName: "IX_faculty-departments_FacultyId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_faculty-departments",
            table: "faculty-departments",
            column: "Id");

        migrationBuilder.CreateTable(
            name: "institute-departments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                InstituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Version = table.Column<long>(type: "bigint", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_institute-departments", x => x.Id);
                table.ForeignKey(
                    name: "FK_institute-departments_institutes_InstituteId",
                    column: x => x.InstituteId,
                    principalTable: "institutes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_institute-departments_InstituteId",
            table: "institute-departments",
            column: "InstituteId");

        migrationBuilder.AddForeignKey(
            name: "FK_faculty-departments_faculties_FacultyId",
            table: "faculty-departments",
            column: "FacultyId",
            principalTable: "faculties",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_faculty-departments_faculties_FacultyId",
            table: "faculty-departments");

        migrationBuilder.DropTable(
            name: "institute-departments");

        migrationBuilder.DropPrimaryKey(
            name: "PK_faculty-departments",
            table: "faculty-departments");

        migrationBuilder.RenameTable(
            name: "faculty-departments",
            newName: "departments");

        migrationBuilder.RenameIndex(
            name: "IX_faculty-departments_FacultyId",
            table: "departments",
            newName: "IX_departments_FacultyId");

        migrationBuilder.AddColumn<Guid>(
            name: "InstituteId",
            table: "departments",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddPrimaryKey(
            name: "PK_departments",
            table: "departments",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_departments_InstituteId",
            table: "departments",
            column: "InstituteId");

        migrationBuilder.AddForeignKey(
            name: "FK_departments_faculties_FacultyId",
            table: "departments",
            column: "FacultyId",
            principalTable: "faculties",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_departments_institutes_InstituteId",
            table: "departments",
            column: "InstituteId",
            principalTable: "institutes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
