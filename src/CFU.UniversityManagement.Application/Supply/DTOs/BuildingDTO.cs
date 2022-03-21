namespace CFU.UniversityManagement.Application.Supply.DTOs;

public class BuildingDTO
{
    public Guid Id { get; set; }
    public AddressDTO Address { get; set; }
    public List<AuditoriumDTO> Auditoriums { get; set; }
}

public class AddressDTO
{
    public string City { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
    public string Block { get; set; }
}
