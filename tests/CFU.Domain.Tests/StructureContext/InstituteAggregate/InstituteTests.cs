using CFU.Domain.StructureContext.InstituteAggregate;

namespace CFU.Domain.UnitTests.StructureContext.InstituteAggregate;

public class InstituteTests
{
    [Theory, InstituteAutoData]
    public void CreateDepartment_ShouldCreateDepartment_WhenAllParametersAreValid(
        Institute institute, DepartmentId id, string title)
    {
        // Act
        var department = institute.CreateDepartment(id, title);

        // Assert
        department.Should().NotBeNull();
        department.Id.Should().Be(id);
        department.Title.Should().Be(title);
        department.InstituteId.Should().Be(institute.Id);
        institute.Departments.Should().Contain(department);
        institute.DomainEvents.Should().ContainEquivalentOf(new InstituteDepartmentCreatedEvent(department));
    }

    [Theory]
    [InstituteMemberAutoData(nameof(GetCreateDepartmentDefaultParameters))]
    public void CreateDepartment_ShouldThrowArgumentException_WhenParametersAreDefault(
        DepartmentId id,
        string title,
        Institute institute)
    {
        // Act
        var act = () => institute.CreateDepartment(id, title);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory, InstituteAutoData]
    public void CreateDepartment_ShouldThrowDepartmentAlreadyExistException_WhenDepartmentAlreadyExist(
        DepartmentId id,
        string title,
        Institute institute)
    {
        // Arrange
        institute.CreateDepartment(id, title);

        // Act
        var act = () => institute.CreateDepartment(id, title);

        // Assert
        act.Should().Throw<DepartmentAlreadyExistException>();
    }

    [Theory, InstituteAutoData]
    public void CreateDepartment_ShouldThrowInstituteAlreadyDisbandedException_WhenInstituteIsDisbanded(
        DepartmentId id,
        string title,
        Institute institute)
    {
        // Arrange
        institute.Disband();

        // Act
        var act = () => institute.CreateDepartment(id, title);

        // Assert
        act.Should().Throw<InstituteAlreadyDisbandedException>();
    }

    [Theory]
    [InstituteAutoData]
    public void Disband_ShouldDisbandInstitute_WhenInstituteIsNotDisbanded(
        Institute institute)
    {
        // Act
        institute.Disband();

        // Assert
        institute.IsDisbanded.Should().BeTrue();
        institute.DomainEvents.Should().ContainEquivalentOf(new InstituteDisbandedEvent(institute));
    }

    [Theory]
    [InstituteAutoData]
    public void Disband_ShouldThrowInstituteAlreadyDisbandedException_WhenInstituteIsDisbanded(
        Institute institute)
    {
        // Assert
        institute.Disband();

        // Act
        var act = () => institute.Disband();

        // Assert
        act.Should().Throw<InstituteAlreadyDisbandedException>();
    }

    public static TheoryData<DepartmentId, string> GetCreateDepartmentDefaultParameters()
    {
        var id = new DepartmentId(Guid.NewGuid());
        var title = "title";

        return new TheoryData<DepartmentId, string>
            {
                { default!, title},
                { id, default!}
            };
    }
}

public class InstituteMemberAutoData : MemberAutoDataAttribute
{
    public InstituteMemberAutoData(string memberName, params object[] parameters)
        : base(new InstituteAutoData(), memberName, parameters) { }
}

public class InstituteAutoData : AutoDataAttribute
{
    public InstituteAutoData() : base(Create) { }

    private static IFixture Create()
    {
        var fixture = new Fixture();

        fixture.Register(() => new Institute(new InstituteId(Guid.NewGuid()), "title"));

        return fixture;
    }
}
