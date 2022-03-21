using CFU.Domain.SupplyContext.StructureUnitAggregate;

namespace CFU.Domain.SupplyContext.BuildingAggregate;

public class Building : Entity<BuildingId>, IAggregateRoot<BuildingId>
{
    private Building(BuildingId id) : base(id) { }

    internal Building(BuildingId id, Address address) : base(id)
    {
        Address = Guard.Against.Default(address, nameof(address));
        IsDecommissioned = false;
    }

    public bool IsDecommissioned { get; private set; }
    public Address Address { get; private set; }

    private readonly List<Auditorium> _auditoriums = new();
    public IReadOnlyCollection<Auditorium> Auditoriums => _auditoriums.AsReadOnly();

    public void Decommission()
    {
        if (IsDecommissioned) throw new BuildingIsAlreadyDecommissionedException(Id);
        IsDecommissioned = true;
    }

    public UnassignedAuditorium AddUnassignedAuditorium(AuditoriumNumber number)
    {
        if (IsDecommissioned) throw new BuildingIsAlreadyDecommissionedException(Id);
        CheckAuditoriumExisting(number);
        var auditorium = new UnassignedAuditorium(number, this);
        _auditoriums.Add(auditorium);
        AddDomainEvent(new AuditoriumAddedEvent(auditorium));
        return auditorium;
    }

    //public AdministrativeAuditorium AddAdministrativeAuditorium(AdministrativeAuditoriumNumber number, StructureUnitId administrativeUnit)
    //{
    //    CheckAuditoriumExisting(number);
    //    var auditorium = new AdministrativeAuditorium(number, Id, administrativeUnit);
    //    _auditoriums.Add(auditorium);
    //    AddDomainEvent(new AuditoriumAddedEvent(auditorium));
    //    return auditorium;
    //}

    public ClassRoom AddClassRoom(AuditoriumNumber number, ClassRoomCapacity capacity)
    {
        if (IsDecommissioned) throw new BuildingIsAlreadyDecommissionedException(Id);
        CheckAuditoriumExisting(number);
        var auditorium = new ClassRoom(number, this, capacity);
        _auditoriums.Add(auditorium);
        AddDomainEvent(new AuditoriumAddedEvent(auditorium));
        return auditorium;
    }

    public void RemoveAuditorium(AuditoriumNumber number)
    {
        if (IsDecommissioned) throw new BuildingIsAlreadyDecommissionedException(Id);
        var auditorium = _auditoriums.FirstOrDefault(a => a.Number == number);
        if (auditorium is null) throw new AuditoriumNotFoundException(number);
        _auditoriums.Remove(auditorium);
        AddDomainEvent(new AuditoriumRemovedEvent(number));
    }

    public ClassRoom ChangeAuditoriumTypeToClassRoom(AuditoriumNumber number, ClassRoomCapacity capacity)
    {
        if (IsDecommissioned) throw new BuildingIsAlreadyDecommissionedException(Id);
        var auditorium = _auditoriums.FirstOrDefault(a => a.Number == number);
        if (auditorium is null) throw new AuditoriumNotFoundException(number);
        if (auditorium is not UnassignedAuditorium) throw new AuditoriumTypeException(number, AuditoriumType.Unassigned);

        var classRoom = new ClassRoom(number, this, capacity);
        _auditoriums.Remove(auditorium);
        _auditoriums.Add(classRoom);
        AddDomainEvent(new AuditoriumTypeChangedEvent(auditorium, classRoom, AuditoriumType.ClassRoom));
        return classRoom;
    }

    public UnassignedAuditorium UnassingClassRoomAuditorium(AuditoriumNumber number)
    {
        if (IsDecommissioned) throw new BuildingIsAlreadyDecommissionedException(Id);
        var auditorium = _auditoriums.FirstOrDefault(a => a.Number == number);
        if (auditorium is null) throw new AuditoriumNotFoundException(number);
        if (auditorium is not ClassRoom) throw new AuditoriumTypeException(number, AuditoriumType.ClassRoom);

        var unassignedAuditorium = new UnassignedAuditorium(number, this);
        _auditoriums.Remove(auditorium);
        _auditoriums.Add(unassignedAuditorium);
        AddDomainEvent(new AuditoriumTypeChangedEvent(auditorium, unassignedAuditorium, AuditoriumType.Unassigned));
        return unassignedAuditorium;
    }

    internal AdministrativeAuditorium ChangeAuditoriumTypeToAdmitistrative(AuditoriumNumber number, StructureUnitId administrativeUnit)
    {
        if (IsDecommissioned) throw new BuildingIsAlreadyDecommissionedException(Id);
        var auditorium = _auditoriums.FirstOrDefault(a => a.Number == number);
        if (auditorium is null) throw new AuditoriumNotFoundException(number);
        if (auditorium is not UnassignedAuditorium) throw new AuditoriumTypeException(number, AuditoriumType.Unassigned);

        var administrativeAuditorium = new AdministrativeAuditorium(number, this, administrativeUnit);
        _auditoriums.Remove(auditorium);
        _auditoriums.Add(administrativeAuditorium);
        AddDomainEvent(new AuditoriumTypeChangedEvent(auditorium, administrativeAuditorium, AuditoriumType.Administrative));
        return administrativeAuditorium;
    }

    internal UnassignedAuditorium UnassingAdministrativeAuditorium(AuditoriumNumber number)
    {
        if (IsDecommissioned) throw new BuildingIsAlreadyDecommissionedException(Id);
        var auditorium = _auditoriums.FirstOrDefault(a => a.Number == number);
        if (auditorium is null) throw new AuditoriumNotFoundException(number);
        if (auditorium is not AdministrativeAuditorium) throw new AuditoriumTypeException(number, AuditoriumType.Administrative);

        var unassignedAuditorium = new UnassignedAuditorium(number, this);
        _auditoriums.Remove(auditorium);
        _auditoriums.Add(unassignedAuditorium);
        AddDomainEvent(new AuditoriumTypeChangedEvent(auditorium, unassignedAuditorium, AuditoriumType.Unassigned));
        return unassignedAuditorium;
    }

    private void CheckAuditoriumExisting(AuditoriumNumber number)
    {
        Guard.Against.Default(number, nameof(number));
        if (_auditoriums.Any(a => a.Number == number))
            throw new AuditoriumAlreadyExistsException(number);
    }
}
