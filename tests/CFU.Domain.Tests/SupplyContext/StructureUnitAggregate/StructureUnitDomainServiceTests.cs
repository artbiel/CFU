using CFU.Domain.SupplyContext.BuildingAggregate;
using CFU.Domain.SupplyContext.StructureUnitAggregate;
using System.Linq;

namespace CFU.Domain.UnitTests.SupplyContext.StructureUnitAggregate;

public class StructureUnitDomainServiceTests
{
    [Theory]
    [StructureUnitDomainServiceAutoData]
    public void AssignToAuditorium_ShouldAssignStructureUnitToAuditorium_WhenParametersAreValid(
        StructureUnitDomainService service, StructureUnit structureUnit, Building building)
    {
        // Arrange
        var auditoriumNumber = new AuditoriumNumber(1);
        var auditorium = building.AddUnassignedAuditorium(auditoriumNumber);

        // Act
        service.AssignToAuditorium(structureUnit, building, auditoriumNumber);
        var administrativeAuditorium = building.Auditoriums.FirstOrDefault(a => a.Number == auditorium.Number) as AdministrativeAuditorium;

        // Assert
        administrativeAuditorium.Should().NotBeNull();
        administrativeAuditorium!.StructureUnitId.Should().Be(structureUnit.Id);
        structureUnit.AuditoriumNumber.Should().Be(auditorium.Number);
        structureUnit.BuildingId.Should().Be(auditorium.BuildingId);
    }

    [Theory]
    [MemberData(nameof(GetAssignToAuditoriumDefaultParameters))]
    public void AssignToAuditorium_ShouldThrowArgumentException_WhenParametersAreDefault(
        StructureUnit structureUnit, Building building, AuditoriumNumber auditoriumNumber)
    {
        // Arrange
        var service = new StructureUnitDomainService();

        // Act
        var act = () => service.AssignToAuditorium(structureUnit, building, auditoriumNumber);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    private static TheoryData<StructureUnit, Building, AuditoriumNumber> GetAssignToAuditoriumDefaultParameters()
    {
        var structureUnit = new StructureUnit(new StructureUnitId(Guid.NewGuid()));
        var building = new Building(new BuildingId(Guid.NewGuid()),
            new Address("fake_city", "fake_street", new BuildingNumber(1), new BuildingBlock("block")));
        var auditoriumNumber = new AuditoriumNumber(1);

        return new TheoryData<StructureUnit, Building, AuditoriumNumber>
            {
                {default!, building, auditoriumNumber },
                {structureUnit, default!, auditoriumNumber },
                {structureUnit, building, default! }
            };
    }
}

public class StructureUnitDomainServiceAutoData : AutoDataAttribute
{
    public StructureUnitDomainServiceAutoData() : base(Create) { }

    private static IFixture Create()
    {
        var fixture = new Fixture();

        var service = new StructureUnitDomainService();
        var id = new BuildingId(Guid.NewGuid());
        var address = new Address("fake_city", "fake_street", new BuildingNumber(1), new BuildingBlock("block"));

        fixture.Inject(service);
        fixture.Register(() => new Building(id, address));
        fixture.Register(() => new StructureUnit(new StructureUnitId(Guid.NewGuid())));

        return fixture;
    }
}
