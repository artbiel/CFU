using AutoMapper;
using CFU.Domain.StructureContext.FacultyAggregate;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.Infrastructure.Structure.Mapping;

public class FacultyProfile : Profile
{
    public FacultyProfile()
    {
        CreateMap<Faculty, FacultyDTO>();
        CreateMap<FacultyId, Guid>().ConstructUsing(id => id.Id);
        CreateMap<Department, DepartmentDTO>();
        CreateMap<DepartmentId, Guid>().ConstructUsing(id => id.Id);
    }
}
