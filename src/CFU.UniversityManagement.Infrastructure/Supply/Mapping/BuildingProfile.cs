using AutoMapper;
using CFU.Domain.SupplyContext.BuildingAggregate;
using CFU.Domain.SupplyContext.StructureUnitAggregate;
using CFU.UniversityManagement.Application.Supply.DTOs;

namespace CFU.UniversityManagement.Infrastructure.Supply.Mapping;

public class BuildingProfile : Profile
{
    public BuildingProfile()
    {
        CreateMap<Building, BuildingDTO>();
        CreateMap<BuildingId, Guid>().ConstructUsing(id => id.Id);
        CreateMap<Address, AddressDTO>();
        CreateMap<BuildingNumber, int>().ConstructUsing(number => number.Value);
        CreateMap<BuildingBlock, string>().ConstructUsing(block => block.Value);
        CreateMap<Auditorium, AuditoriumDTO>();
        CreateMap<AuditoriumNumber, int>().ConstructUsing(number => number.Value);
        CreateMap<UnassignedAuditorium, UnassignedAuditoriumDTO>();
        CreateMap<AdministrativeAuditorium, AdministrativeAuditoriumDTO>();
        CreateMap<StructureUnitId, Guid>().ConstructUsing(id => id.Id);
        CreateMap<ClassRoom, ClassRoomDTO>();
        CreateMap<ClassRoomCapacity, int>().ConstructUsing(capacity => capacity.Value);
    }
}
