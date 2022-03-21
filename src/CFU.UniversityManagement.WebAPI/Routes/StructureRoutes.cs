namespace CFU.UniversityManagement.WebAPI.Routes;

public abstract class StructureRoutes : BaseRoute
{
    public const string Structure = $"{Base}/structure";
    public const string Academy = $"{Base}/academy";
    public const string AcademyById = $"{Base}/academy/{{Id}}";
    public const string AcademyFaculties = $"{Base}/academy{{AcademyId}}/faculties";
    public const string Institute = $"{Base}/institute";
    public const string InstituteById = $"{Base}/institute/{{Id}}";
    public const string InstituteDepartments = $"{Base}/institute{{FacultyId}}/departments";
    public const string FacultyById = $"{Base}/faculty/{{Id}}";
    public const string FacultyDepartments = $"{Base}/faculty{{FacultyId}}/departments";
    public const string DepartmentById = $"{Base}/department/{{Id}}";
}
