using CFU.Domain.StudyContext;
using CFU.Domain.StudyContext.DepartmentAggregate;

namespace CFU.Domain.UnitTests.StudyContext.DepartmentAggregate;

public class DepartmentTests
{
    [Fact]
    public void AddSpeciality_ShouldAddSpeciality_WhenAllParametersAreValidAndSpecialityNotExists()
    {
        // Arrange
        var department = new Department(new DepartmentId(Guid.NewGuid()));

        var id = new SpecialityId(Guid.NewGuid());
        var title = "title";
        var attendanceType = AttendanceType.Attended;
        var degree = Degree.Baccalaureate;

        // Act
        var speciality = department.AddSpeciality(id, title, attendanceType, degree);

        // Assert
        speciality.Should().NotBeNull();
        speciality.Id.Should().Be(id);
        speciality.DepartmentId.Should().Be(department.Id);
        speciality.Title.Should().Be(title);
        speciality.AttendanceType.Should().Be(attendanceType);
        speciality.Degree.Should().Be(degree);
        department.Specialities.Should().Contain(speciality);
        department.DomainEvents.Should().ContainEquivalentOf(new SpecialityAddedEvent(speciality));
    }

    [Theory]
    [MemberData(nameof(GetSpecialityDefaultParameters))]
    public void AddSpeciality_ShouldThrowArgumentException_WhenParametersAreDefault(
        SpecialityId id, string title, AttendanceType attendanceType, Degree degree)
    {
        // Arrange
        var department = new Department(new DepartmentId(Guid.NewGuid()));

        // Act
        var act = () => department.AddSpeciality(id, title, attendanceType, degree);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void AddSpeciality_ShouldThrowSpecialityAlreadyExistsException_WhenSpecialityAlreadyExists()
    {
        // Arrange
        var department = new Department(new DepartmentId(Guid.NewGuid()));

        var id = new SpecialityId(Guid.NewGuid());
        var title = "title";
        var attendanceType = AttendanceType.Attended;
        var degree = Degree.Baccalaureate;
        department.AddSpeciality(id, title, attendanceType, degree);

        // Act
        var act = () => department.AddSpeciality(id, title, attendanceType, degree);

        // Assert
        act.Should().Throw<SpecialityAlreadyExistsException>();
    }

    [Fact]
    public void RemoveSpeciality_ShouldRemoveSpeciality_WhenSpecialityExists()
    {
        // Arrange
        var department = new Department(new DepartmentId(Guid.NewGuid()));

        var id = new SpecialityId(Guid.NewGuid());
        var title = "title";
        var attendanceType = AttendanceType.Attended;
        var degree = Degree.Baccalaureate;
        var speciality = department.AddSpeciality(id, title, attendanceType, degree);

        // Act
        department.RemoveSpeciality(id);

        // Assert
        department.Specialities.Should().NotContain(speciality);
        department.DomainEvents.Should().ContainEquivalentOf(new SpecialityRemovedEvent(speciality));
    }

    [Fact]
    public void RemoveSpeciality_ShouldThrowSpecialityNotFoundException_WhenSpecialityNotExists()
    {
        // Arrange
        var department = new Department(new DepartmentId(Guid.NewGuid()));

        // Act
        var act = () => department.RemoveSpeciality(default);

        // Assert
        act.Should().Throw<SpecialityNotFoundException>();
    }

    private static TheoryData<SpecialityId, string, AttendanceType, Degree> GetSpecialityDefaultParameters()
    {
        var id = new SpecialityId(Guid.NewGuid());
        var title = "title";
        var attendance = AttendanceType.Attended;
        var degree = Degree.Baccalaureate;

        return new TheoryData<SpecialityId, string, AttendanceType, Degree>
            {
                { default!, title, attendance, degree },
                { id, default!, attendance, degree },
                { id, "", attendance, degree },
                { id, "  ", attendance, degree },
                { id, title, default!, degree },
                { id, title, attendance, default! },
            };
    }
}
