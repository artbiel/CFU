namespace CFU.Domain.SupplyContext.BuildingAggregate;

public record struct BuildingNumber
{
    public BuildingNumber(int buildingNumber)
    {
        Value = Guard.Against.NegativeOrZero(buildingNumber, nameof(buildingNumber));
    }

    public int Value { get; private init; }
}

