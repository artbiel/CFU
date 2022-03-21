namespace CFU.Domain.SupplyContext.BuildingAggregate;

public class BuildingAlreadyExistsException : DomainException
{
    public BuildingAlreadyExistsException(Address address)
        : base($"Building with address {address} already exists") { }
}
