using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.Application.StructureContext.Queries;

public record GetUniversityStructureQuery : ICacheableQuery<UniversityStructureDTO>
{
    public string CacheKey => nameof(GetUniversityStructureQuery);

    public TimeSpan AbsoluteExpirationRelativeToNow => new TimeSpan(1, 0, 0);
}
