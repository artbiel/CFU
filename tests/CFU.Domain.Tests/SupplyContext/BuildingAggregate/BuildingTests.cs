using CFU.Domain.SupplyContext.BuildingAggregate;
using CFU.Domain.SupplyContext.StructureUnitAggregate;

namespace CFU.Domain.UnitTests.SupplyContext.BuildingAggregate;

public class BuildingTests
{
    [Theory, BuildingAutoData]
    public void AddUnassignedAuditorium_ShouldAddUnassignedAuditorium_WhenAllParametersAreValid(Building building)
    {
        // Arrange
        var number = new AuditoriumNumber(1);

        // Act
        var audithorium = building.AddUnassignedAuditorium(number);

        // Assert
        audithorium.Should().NotBeNull();
        audithorium.Number.Should().BeEquivalentTo(number);
        audithorium.BuildingId.Should().BeEquivalentTo(building.Id);
        building.Auditoriums.Should().ContainSingle();
        building.Auditoriums.Should().Contain(audithorium);
        building.DomainEvents.Should().ContainEquivalentOf(new AuditoriumAddedEvent(audithorium));
    }

    [Theory, BuildingAutoData]
    public void AddUnassignedAuditorium_ShouldThrowArgumentException_WhenParametersAreDefault(Building building)
    {
        // Arrange
        AuditoriumNumber auditoriumNumber = default!;

        // Act
        Action act = () => { var audithorium = building.AddUnassignedAuditorium(auditoriumNumber); };

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory, BuildingAutoData]
    public void AddUnassignedAuditorium_ShouldThrowAuditoriumAlreadyExistsException_WhenAuditoriumAlreadyExists(Building building)
    {
        // Arrange
        var auditoriumNumber = new AuditoriumNumber(1);
        building.AddUnassignedAuditorium(auditoriumNumber);

        // Act
        Action act = () => { var audithorium = building.AddUnassignedAuditorium(auditoriumNumber); };

        // Assert
        act.Should().Throw<AuditoriumAlreadyExistsException>();
    }

    [Theory, BuildingAutoData]
    public void AddClassRoom_ShouldAddClassRoom_WhenAllParametersAreValid(Building building)
    {
        // Arrange
        var number = new AuditoriumNumber(1);
        var classRoomCapacity = new ClassRoomCapacity(1);

        // Act
        var audithorium = building.AddClassRoom(number, classRoomCapacity);

        // Assert
        audithorium.Should().NotBeNull();
        audithorium.Number.Should().Be(number);
        audithorium.BuildingId.Should().Be(building.Id);
        audithorium.Capacity.Should().Be(classRoomCapacity);
        building.Auditoriums.Should().HaveCount(1);
        building.Auditoriums.Should().Contain(audithorium);
        building.DomainEvents.Should().ContainEquivalentOf(new AuditoriumAddedEvent(audithorium));
    }

    [Theory]
    [BuildingAutoData]
    public void AddClassRoom_ShouldThrowArgumentException_WhenNumberIsDefault(Building building)
    {
        AuditoriumNumber number = default;
        var capacity = new ClassRoomCapacity(1);

        // Act
        Action act = () => { var auditorium = building.AddClassRoom(number, capacity); };

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory, BuildingAutoData]
    public void AddClassRoom_ShouldThrowAuditoriumAlreadyExistsException_WhenAuditoriumAlreadyExists(Building building)
    {
        // Arrange
        var auditoriumNumber = 1;
        building.AddUnassignedAuditorium(new AuditoriumNumber(auditoriumNumber));

        // Act
        Action act = () => {
            var audithorium = building.AddClassRoom(
new AuditoriumNumber(auditoriumNumber),
new ClassRoomCapacity(ClassRoomCapacity.MIN_CAPACITY));
        };

        // Assert
        act.Should().Throw<AuditoriumAlreadyExistsException>();
    }

    [Theory]
    [BuildingAutoData]
    public void RemoveAuditorium_ShouldRemoveAuditorium_WhenBuildingContainsAuditorium(Building building)
    {
        //Assert
        var auditoriumNumber = new AuditoriumNumber(1);
        var auditorium = building.AddUnassignedAuditorium(auditoriumNumber);

        // Act
        building.RemoveAuditorium(auditoriumNumber);

        // Assert
        building.Auditoriums.Should().NotContain(auditorium);
        building.DomainEvents.Should().ContainEquivalentOf(new AuditoriumRemovedEvent(auditoriumNumber));
    }

    [Theory]
    [BuildingAutoData]
    public void RemoveAuditorium_ShouldThrowAuditoriumNotFoundException_WhenBuildingNotContainsAuditorium(Building building)
    {
        //Assert
        var auditoriumNumber = new AuditoriumNumber(1);

        // Act
        Action act = () => building.RemoveAuditorium(auditoriumNumber);

        // Assert
        act.Should().Throw<AuditoriumNotFoundException>();
    }

    [Theory]
    [BuildingAutoData]
    public void ChangeAuditoriumTypeToClassRoom_ShouldChangeAuditoriumTypeToClassRoom_WhenAuditoriumIsUnassigned(Building building)
    {
        //Assert
        var auditoriumNumber = new AuditoriumNumber(1);
        var auditorium = building.AddUnassignedAuditorium(auditoriumNumber);
        var classRoomCapacity = new ClassRoomCapacity(ClassRoomCapacity.MIN_CAPACITY);

        // Act
        var classRoom = building.ChangeAuditoriumTypeToClassRoom(auditoriumNumber, classRoomCapacity);

        // Assert
        building.Auditoriums.Should().NotContain(auditorium);
        building.Auditoriums.Should().ContainSingle();
        building.Auditoriums.Should().Contain(classRoom);
        classRoom.Number.Should().Be(auditorium.Number);
        classRoom.BuildingId.Should().Be(auditorium.BuildingId);
        classRoom.Capacity.Should().Be(classRoomCapacity);
        building.DomainEvents.Should().ContainEquivalentOf(new AuditoriumTypeChangedEvent(auditorium, classRoom, AuditoriumType.ClassRoom));
    }

    [Theory]
    [BuildingAutoData]
    public void ChangeAuditoriumTypeToClassRoom_ShouldThrowAuditoriumTypeException_WhenAuditoriumIsNotUnassigned(Building building)
    {
        //Assert
        var auditoriumNumber = new AuditoriumNumber(1);
        var classRoomCapacity = new ClassRoomCapacity(ClassRoomCapacity.MIN_CAPACITY);
        var auditorium = building.AddClassRoom(auditoriumNumber, classRoomCapacity);

        // Act
        Action act = () => building.ChangeAuditoriumTypeToClassRoom(auditoriumNumber, classRoomCapacity);

        // Assert
        act.Should().Throw<AuditoriumTypeException>();
    }

    [Theory]
    [BuildingAutoData]
    public void ChangeAuditoriumTypeToClassRoom_ShouldThrowAuditoriumNotFoundException_WhenAuditoriumNotFound(Building building)
    {
        //Assert
        var auditoriumNumber = new AuditoriumNumber(1);
        var classRoomCapacity = new ClassRoomCapacity(ClassRoomCapacity.MIN_CAPACITY);

        // Act
        Action act = () => building.ChangeAuditoriumTypeToClassRoom(auditoriumNumber, classRoomCapacity);

        // Assert
        act.Should().Throw<AuditoriumNotFoundException>();
    }

    [Theory]
    [BuildingAutoData]
    public void UnassingClassRoomAuditorium_ShouldChangeAuditoriumTypeToUnassigned_WhenParametersAreValid(Building building)
    {
        //Assert
        var auditoriumNumber = new AuditoriumNumber(1);
        var capacity = new ClassRoomCapacity(ClassRoomCapacity.MIN_CAPACITY);
        var auditorium = building.AddClassRoom(auditoriumNumber, capacity);

        // Act
        var unassignedAuditorium = building.UnassingClassRoomAuditorium(auditoriumNumber);

        // Assert
        building.Auditoriums.Should().NotContain(auditorium);
        building.Auditoriums.Should().ContainSingle();
        building.Auditoriums.Should().Contain(unassignedAuditorium);
        unassignedAuditorium.Number.Should().Be(auditorium.Number);
        unassignedAuditorium.BuildingId.Should().Be(auditorium.BuildingId);
        building.DomainEvents.Should().ContainEquivalentOf(new AuditoriumTypeChangedEvent(auditorium, unassignedAuditorium, AuditoriumType.Unassigned));
    }

    [Theory]
    [BuildingAutoData]
    public void UnassingClassRoomAuditorium_ShouldThrowAuditoriumTypeException_WhenAuditoriumIsNotClassRoom(Building building)
    {
        //Assert
        var auditoriumNumber = new AuditoriumNumber(1);
        var auditorium = building.AddUnassignedAuditorium(auditoriumNumber);

        // Act
        Action act = () => building.UnassingClassRoomAuditorium(auditoriumNumber);

        // Assert
        act.Should().Throw<AuditoriumTypeException>();
    }

    // Internal

    [Theory]
    [BuildingAutoData]
    public void ChangeAuditoriumTypeToAdmitistrative_ShouldChangeAuditoriumTypeToAdmitistrative_WhenParametersAreValid(Building building)
    {
        //Assert
        var auditoriumNumber = new AuditoriumNumber(1);
        var auditorium = building.AddUnassignedAuditorium(auditoriumNumber);
        var unitId = new StructureUnitId(Guid.NewGuid());

        // Act
        var administrativeAuditorium = building.ChangeAuditoriumTypeToAdmitistrative(auditoriumNumber, unitId);

        // Assert
        building.Auditoriums.Should().NotContain(auditorium);
        building.Auditoriums.Should().ContainSingle();
        building.Auditoriums.Should().Contain(administrativeAuditorium);
        administrativeAuditorium.Number.Should().Be(auditorium.Number);
        administrativeAuditorium.BuildingId.Should().Be(auditorium.BuildingId);
        administrativeAuditorium.StructureUnitId.Should().Be(unitId);
        building.DomainEvents.Should().ContainEquivalentOf(new AuditoriumTypeChangedEvent(auditorium, administrativeAuditorium, AuditoriumType.Administrative));
    }

    [Theory]
    [BuildingAutoData]
    public void ChangeAuditoriumTypeToAdmitistrative_ShouldThrowAuditoriumTypeException_WhenAuditoriumIsNotUnassigned(Building building)
    {
        //Assert
        var auditoriumNumber = new AuditoriumNumber(1);
        var classRoomCapacity = new ClassRoomCapacity(ClassRoomCapacity.MIN_CAPACITY);
        var auditorium = building.AddClassRoom(auditoriumNumber, classRoomCapacity);
        var structureUnitId = new StructureUnitId(Guid.NewGuid());

        // Act
        Action act = () => building.ChangeAuditoriumTypeToAdmitistrative(auditoriumNumber, structureUnitId);

        // Assert
        act.Should().Throw<AuditoriumTypeException>();
    }

    public static TheoryData<BuildingId, Address, IBuildingRepository> GetBuildingDefaultParameters()
    {
        var repository = Substitute.For<IBuildingRepository>();
        var id = new BuildingId(Guid.NewGuid());
        var address = new Address("fake_city", "fake_street", new BuildingNumber(1), new BuildingBlock("block"));
        return new TheoryData<BuildingId, Address, IBuildingRepository>
            {
                { default!, address, repository },
                { id, default!, repository },
                { id, address, default! }
            };
    }

    public static TheoryData<AuditoriumNumber, StructureUnitId> GetAdministrativeAuditoriumDefaultParameters()
    {
        var number = new AuditoriumNumber(1);
        var structureUnitId = new StructureUnitId(Guid.NewGuid());

        return new TheoryData<AuditoriumNumber, StructureUnitId>
            {
                { number, default! },
                { default!, structureUnitId }
            };
    }
}

public class BuildingMemberAutoData : MemberAutoDataAttribute
{
    public BuildingMemberAutoData(string memberName, params object[] parameters) : base(new BuildingAutoData(), memberName, parameters) { }
}

public class BuildingAutoData : AutoDataAttribute
{
    public BuildingAutoData() : base(Create) { }

    private static IFixture Create()
    {
        var fixture = new Fixture();

        var id = new BuildingId(Guid.NewGuid());
        var address = new Address("fake_city", "fake_street", new BuildingNumber(1), new BuildingBlock("block"));

        fixture.Inject(id);
        fixture.Inject(address);
        fixture.Register(() => new Building(id, address));

        return fixture;
    }
}
