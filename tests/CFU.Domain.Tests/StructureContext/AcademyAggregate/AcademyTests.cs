using CFU.Domain.StructureContext.AcademyAggregate;

namespace CFU.Domain.UnitTests.StructureContext.AcademyAggregate;

public class AcademyTests
{
    [Theory, AcademyAutoData]
    public void CreateFaculty_ShouldCreateFaculty_WhenAllParametersAreValid(
        Academy academy, FacultyId id, string title)
    {
        // Act
        var faculty = academy.CreateFaculty(id, title);

        // Assert
        faculty.Should().NotBeNull();
        faculty.Id.Should().Be(id);
        faculty.Title.Should().Be(title);
        faculty.AcademyId.Should().Be(academy.Id);
        academy.Faculties.Should().Contain(faculty);
        academy.DomainEvents.Should().ContainEquivalentOf(new FacultyCreatedEvent(faculty));
    }

    [Theory]
    [AcademyMemberAutoData(nameof(GetCreateFacultyDefaultParameters))]
    public void CreateFaculty_ShouldThrowArgumentException_WhenParametersAreDefault(
        FacultyId id,
        string title,
        Academy academy)
    {
        // Act
        var act = () => academy.CreateFaculty(id, title);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory, AcademyAutoData]
    public void CreateFaculty_ShouldThrowFacultyAlreadyExistException_WhenFacultyAlreadyExist(
        FacultyId id,
        string title,
        Academy academy)
    {
        // Arrange
        academy.CreateFaculty(id, title);

        // Act
        var act = () => academy.CreateFaculty(id, title);

        // Assert
        act.Should().Throw<FacultyAlreadyExistException>();
    }

    [Theory, AcademyAutoData]
    public void CreateFaculty_ShouldThrowAcademyAlreadyDisbandedException_WhenAcademyIsDisbanded(
        FacultyId id,
        string title,
        Academy academy)
    {
        // Arrange
        academy.Disband();

        // Act
        var act = () => academy.CreateFaculty(id, title);

        // Assert
        act.Should().Throw<AcademyAlreadyDisbandedException>();
    }

    [Theory]
    [AcademyAutoData]
    public void Disband_ShouldDisbandAcademy_WhenAcademyIsNotDisbanded(
       Academy academy)
    {
        // Act
        academy.Disband();

        // Assert
        academy.IsDisbanded.Should().BeTrue();
        academy.DomainEvents.Should().ContainEquivalentOf(new AcademyDisbandedEvent(academy));
    }

    [Theory]
    [AcademyAutoData]
    public void Disband_ShouldThrowAcademyAlreadyDisbandedException_WhenAcademyIsDisbanded(
        Academy academy)
    {
        // Assert
        academy.Disband();

        // Act
        var act = () => academy.Disband();

        // Assert
        act.Should().Throw<AcademyAlreadyDisbandedException>();
    }

    public static TheoryData<FacultyId, string> GetCreateFacultyDefaultParameters()
    {
        var id = new FacultyId(Guid.NewGuid());
        var title = "title";

        return new TheoryData<FacultyId, string>
            {
                { default!, title},
                { id, default!}
            };
    }
}

public class AcademyMemberAutoData : MemberAutoDataAttribute
{
    public AcademyMemberAutoData(string memberName, params object[] parameters)
        : base(new AcademyAutoData(), memberName, parameters) { }
}

public class AcademyAutoData : AutoDataAttribute
{
    public AcademyAutoData() : base(Create) { }

    private static IFixture Create()
    {
        var fixture = new Fixture();

        fixture.Register(() => new Academy(new AcademyId(Guid.NewGuid()), "title"));

        return fixture;
    }
}
