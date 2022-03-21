using CFU.Domain.StructureContext.FacultyAggregate;

namespace CFU.Domain.UnitTests.StructureContext.FacultyAggregate;

public class FacultyTests
{
    [Theory, FacultyAutoData]
    public void CreateDepartment_ShouldCreateDepartment_WhenAllParametersAreValid(
        Faculty faculty, DepartmentId id, string title)
    {
        // Act
        var department = faculty.CreateDepartment(id, title);

        // Assert
        department.Should().NotBeNull();
        department.Id.Should().Be(id);
        department.Title.Should().Be(title);
        department.FacultyId.Should().Be(faculty.Id);
        faculty.Departments.Should().Contain(department);
        faculty.DomainEvents.Should().ContainEquivalentOf(new FacultyDepartmentCreatedEvent(department));
    }

    [Theory]
    [FacultyMemberAutoData(nameof(GetCreateDepartmentDefaultParameters))]
    public void CreateDepartment_ShouldThrowArgumentException_WhenParametersAreDefault(
        DepartmentId id,
        string title,
        Faculty faculty)
    {
        // Act
        var act = () => faculty.CreateDepartment(id, title);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory, FacultyAutoData]
    public void CreateDepartment_ShouldThrowDepartmentAlreadyExistException_WhenDepartmentAlreadyExist(
        DepartmentId id,
        string title,
        Faculty faculty)
    {
        // Arrange
        faculty.CreateDepartment(id, title);

        // Act
        var act = () => faculty.CreateDepartment(id, title);

        // Assert
        act.Should().Throw<DepartmentAlreadyExistException>();
    }

    [Theory, FacultyAutoData]
    public void DisbandDepartment_ShouldDisbandDepartment_WhenAllParametersAreValid(
        Faculty faculty, DepartmentId id, string title)
    {
        // Assert
        var department = faculty.CreateDepartment(id, title);

        // Act
        faculty.DisbandDepartment(id);

        // Assert
        faculty.Departments.Should().NotContain(department);
        faculty.DomainEvents.Should().ContainEquivalentOf(new FacultyDepartmentDisbandedEvent(department));
    }

    [Theory]
    [FacultyAutoData]
    public void DisbandDepartment_ShouldThrowDepartmentNotFoundException_WhenDepartmentNotFound(
        DepartmentId id,
        Faculty faculty)
    {
        // Act
        var act = () => faculty.DisbandDepartment(id);

        // Assert
        act.Should().Throw<DepartmentNotFoundException>();
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

public class FacultyMemberAutoData : MemberAutoDataAttribute
{
    public FacultyMemberAutoData(string memberName, params object[] parameters)
        : base(new FacultyAutoData(), memberName, parameters) { }
}

public class FacultyAutoData : AutoDataAttribute
{
    public FacultyAutoData() : base(Create) { }

    private static IFixture Create()
    {
        var fixture = new Fixture();

        fixture.Register(() => new Faculty(new FacultyId(Guid.NewGuid()), "title"));

        return fixture;
    }
}
