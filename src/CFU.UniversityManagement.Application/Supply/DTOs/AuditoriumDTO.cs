namespace CFU.UniversityManagement.Application.Supply.DTOs;

public abstract class AuditoriumDTO
{
    public int Number { get; set; }
}

public class UnassignedAuditoriumDTO : AuditoriumDTO { }

public class AdministrativeAuditoriumDTO : AuditoriumDTO
{
    public Guid StructureUnitId { get; set; }
}

public class ClassRoomDTO : AuditoriumDTO
{
    public int Capacity { get; set; }
}
