using AutoMapper;
using CFU.Domain.StructureContext.InstituteAggregate;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.Infrastructure.Structure.Mapping;

public class InstituteProfile : Profile
{
    public InstituteProfile()
    {
        CreateMap<Institute, InstituteDTO>();
        CreateMap<InstituteId, Guid>().ConstructUsing(id => id.Id);
        CreateMap<Department, DepartmentDTO>();
        CreateMap<DepartmentId, Guid>().ConstructUsing(id => id.Id);
    }
}
