using AutoMapper;
using CFU.Domain.StructureContext.AcademyAggregate;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.Infrastructure.Structure.Mapping;

public class AcademyProfile : Profile
{
    public AcademyProfile()
    {
        CreateMap<Academy, AcademyDTO>();
        CreateMap<AcademyId, Guid>().ConstructUsing(id => id.Id);
        CreateMap<Faculty, FacultyDTO>();
        CreateMap<FacultyId, Guid>().ConstructUsing(id => id.Id);
    }
}
