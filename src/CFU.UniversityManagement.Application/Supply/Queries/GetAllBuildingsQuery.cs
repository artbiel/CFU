using CFU.UniversityManagement.Application.Supply.DTOs;

namespace CFU.UniversityManagement.Application.Supply.Queries;

public record GetAllBuildingsQuery() : IQuery<IEnumerable<BuildingDTO>>;
