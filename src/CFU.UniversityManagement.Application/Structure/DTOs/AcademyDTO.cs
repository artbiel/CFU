namespace CFU.UniversityManagement.Application.Structure.DTOs;

public class AcademyDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<FacultyDTO> Faculties { get; set; } = new();
}
