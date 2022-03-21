namespace CFU.UniversityManagement.Application.Structure.DTOs;

public record InstituteDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<DepartmentDTO> Departments { get; set; } = new();
}
