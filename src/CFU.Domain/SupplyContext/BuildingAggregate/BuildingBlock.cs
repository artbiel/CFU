namespace CFU.Domain.SupplyContext.BuildingAggregate;

public record BuildingBlock
{
    private BuildingBlock() => Value = null;

    public BuildingBlock(string block)
    {
        Value = Guard.Against.NullOrWhiteSpace(block, nameof(block));
    }

    public static BuildingBlock None => new BuildingBlock();

    public string Value { get; }
}

