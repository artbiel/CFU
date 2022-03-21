namespace CFU.Domain.SupplyContext.BuildingAggregate;

public class ClassRoom : Auditorium
{
    private ClassRoom() { }

    internal ClassRoom(AuditoriumNumber number, Building building, ClassRoomCapacity capacity) : base(number, building)
    {
        SetCapacity(capacity);
    }

    public ClassRoomCapacity Capacity { get; private set; }

    public void SetCapacity(ClassRoomCapacity capacity)
    {
        Capacity = capacity;
    }
}

