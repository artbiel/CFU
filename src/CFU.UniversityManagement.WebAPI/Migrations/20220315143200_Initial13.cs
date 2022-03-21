using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CFU.UniversityManagement.WebAPI.Migrations;

public partial class Initial13 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_institute-departments_institutes_InstituteId",
            table: "institute-departments");

        migrationBuilder.DropTable(
            name: "faculty-departments");

        migrationBuilder.DropPrimaryKey(
            name: "PK_institute-departments",
            table: "institute-departments");

        migrationBuilder.RenameTable(
            name: "institute-departments",
            newName: "departments");

        migrationBuilder.RenameIndex(
            name: "IX_institute-departments_InstituteId",
            table: "departments",
            newName: "IX_departments_InstituteId");

        migrationBuilder.AddColumn<bool>(
            name: "IsDisbanded",
            table: "faculties",
            type: "bit",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDecommissioned",
            table: "buildings",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "FacultyId",
            table: "departments",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDisbanded",
            table: "departments",
            type: "bit",
            nullable: true);

        migrationBuilder.AddPrimaryKey(
            name: "PK_departments",
            table: "departments",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_departments_FacultyId",
            table: "departments",
            column: "FacultyId");

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

    protected override void Down(MigrationBuilder migrationBuilder)
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
            name: "IX_departments_FacultyId",
            table: "departments");

        migrationBuilder.DropColumn(
            name: "IsDisbanded",
            table: "faculties");

        migrationBuilder.DropColumn(
            name: "IsDecommissioned",
            table: "buildings");

        migrationBuilder.DropColumn(
            name: "FacultyId",
            table: "departments");

        migrationBuilder.DropColumn(
            name: "IsDisbanded",
            table: "departments");

        migrationBuilder.RenameTable(
            name: "departments",
            newName: "institute-departments");

        migrationBuilder.RenameIndex(
            name: "IX_departments_InstituteId",
            table: "institute-departments",
            newName: "IX_institute-departments_InstituteId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_institute-departments",
            table: "institute-departments",
            column: "Id");

        migrationBuilder.CreateTable(
            name: "faculty-departments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FacultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Version = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_faculty-departments", x => x.Id);
                table.ForeignKey(
                    name: "FK_faculty-departments_faculties_FacultyId",
                    column: x => x.FacultyId,
                    principalTable: "faculties",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_faculty-departments_FacultyId",
            table: "faculty-departments",
            column: "FacultyId");

        migrationBuilder.AddForeignKey(
            name: "FK_institute-departments_institutes_InstituteId",
            table: "institute-departments",
            column: "InstituteId",
            principalTable: "institutes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
