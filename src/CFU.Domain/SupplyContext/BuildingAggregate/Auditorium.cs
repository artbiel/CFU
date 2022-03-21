namespace CFU.Domain.SupplyContext.BuildingAggregate;

public abstract class Auditorium : Entity/*, IEquatable<Auditorium>*/
{
    protected Auditorium() { } // For EF

    protected Auditorium(AuditoriumNumber number, Building building)
    {
        Number = Guard.Against.Default(number, nameof(number));
        BuildingId = Guard.Against.Default(building, nameof(building)).Id;
    }

    public AuditoriumNumber Number { get; private init; }
    public BuildingId BuildingId { get; private init; }

    //public bool Equals(Auditorium other) => Number == other.Number && BuildingId == other.BuildingId;
    //public override bool Equals(object obj) => obj is Auditorium a && Number == a.Number && BuildingId == a.BuildingId;
    //public override int GetHashCode() => Number.GetHashCode() ^ BuildingId.GetHashCode();
}
