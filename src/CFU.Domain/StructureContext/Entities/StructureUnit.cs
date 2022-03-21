namespace CFU.Domain.StructureContext.Entities;

public abstract class StructureUnit<TId> : Entity<TId>
{
    protected StructureUnit(TId id, string title) : base(id)
    {
        Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
    }

    public string Title { get; private set; }

    public override bool Equals(object obj) => obj is StructureUnit<TId> unit && Title.Equals(unit.Title, StringComparison.OrdinalIgnoreCase);
    public override int GetHashCode() => Title.GetHashCode();
}
