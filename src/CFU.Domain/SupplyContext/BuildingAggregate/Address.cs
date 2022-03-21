namespace CFU.Domain.SupplyContext.BuildingAggregate;

public record Address
{
    public Address(string city, string street, BuildingNumber buildingNumber, BuildingBlock block)
    {
        City = Guard.Against.NullOrWhiteSpace(city, nameof(city));
        Street = Guard.Against.NullOrWhiteSpace(street, nameof(street));
        BuildingNumber = Guard.Against.Default(buildingNumber, nameof(buildingNumber));
        Block = block;
    }

    public string City { get; private init; }
    public string Street { get; private init; }
    public BuildingNumber BuildingNumber { get; private init; }
    public BuildingBlock Block { get; private init; }
}

