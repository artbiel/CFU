using System.Linq.Expressions;

namespace CFU.Domain.SupplyContext.BuildingAggregate;

public record UniqueBuildingSpecification : Specification<Building>
{
    private readonly Address _address;

    public UniqueBuildingSpecification(Address address)
    {
        _address = Guard.Against.Default(address, nameof(address));
    }

    public override Expression<Func<Building, bool>> ToExpression() =>
        b => b.Address.City.ToUpper() == _address.City.ToUpper()
        && b.Address.Street.ToUpper() == _address.Street.ToUpper()
        && b.Address.BuildingNumber == _address.BuildingNumber
        && b.Address.Block == _address.Block;
}
