namespace CFU.UniversityManagement.Application.Structure.DTOs;

[Serializable]
public class UniversityStructureDTO
{
    public AcademyDTO[] Academies { get; set; }
    public InstituteDTO[] Institutes { get; set; }
}
