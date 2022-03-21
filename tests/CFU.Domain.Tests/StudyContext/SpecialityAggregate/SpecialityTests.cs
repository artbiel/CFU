using CFU.Domain.StudyContext;
using CFU.Domain.StudyContext.SpecialityAggregate;

namespace CFU.Domain.UnitTests.StudyContext.SpecialityAggregate;

public class SpecialityTests
{
    [Fact]
    public void AddGroup_ShouldAddGroup_WhenAllParametersAreValidAndGroupNotExists()
    {
        // Arrange
        var id = new SpecialityId(Guid.NewGuid());
        var title = "Информатика и Вычислительная Техника";
        var attendanceType = AttendanceType.Attended;
        var degree = Degree.Baccalaureate;
        var speciality = new Speciality(id, title, attendanceType, degree);

        var groupId = new GroupId(Guid.NewGuid());
        var yearOfAdmission = new YearOfAdmission(2022);

        // Act
        var group = speciality.AddGroup(groupId, yearOfAdmission);

        // Assert
        group.Should().NotBeNull();
        group.Id.Should().Be(groupId);
        group.Title.Should().Be("ИВТ-б-о-22");
        group.SpecialityId.Should().Be(speciality.Id);
        group.YearOfAdmission.Should().Be(yearOfAdmission);
        speciality.Groups.Should().Contain(group);
        speciality.DomainEvents.Should().ContainEquivalentOf(new GroupAddedToSpecialityEvent(speciality, group));
    }

    [Theory]
    [SpecialityMemberAutoData(nameof(GetGroupDefaultParameters))]
    public void AddGroup_ShouldThrowArgumentException_WhenParametersAreDefault(
        GroupId id, YearOfAdmission yearOfAdmission, Speciality speciality)
    {
        // Act
        var act = () => speciality.AddGroup(id, yearOfAdmission);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [SpecialityAutoData]
    public void AddGroup_ShouldThrowGroupAlreadyExistsException_WhenGroupAlreadyExists(Speciality speciality)
    {
        // Arrange
        var id = new GroupId(Guid.NewGuid());
        var yearOfAdmission = new YearOfAdmission(2022);
        speciality.AddGroup(id, yearOfAdmission);

        // Act
        var act = () => speciality.AddGroup(id, yearOfAdmission);

        // Assert
        act.Should().Throw<GroupAlreadyExistsException>();
    }

    [Theory]
    [SpecialityAutoData]
    public void RemoveGroup_ShouldRemoveGroup_WhenGroupExists(Speciality speciality)
    {
        // Arrange
        var id = new GroupId(Guid.NewGuid());
        var yearOfAdmission = new YearOfAdmission(2022);
        var group = speciality.AddGroup(id, yearOfAdmission);

        // Act
        speciality.RemoveGroup(id);

        // Assert
        speciality.Groups.Should().NotContain(group);
        speciality.DomainEvents.Should().ContainEquivalentOf(new GroupRemovedFromSpecialityEvent(speciality, group));
    }

    [Theory]
    [SpecialityAutoData]
    public void RemoveGroup_ShouldThrowGroupNotFoundException_WhenGroupNotExists(Speciality speciality)
    {
        // Act
        var act = () => speciality.RemoveGroup(default);

        // Assert
        act.Should().Throw<GroupNotFoundException>();
    }

    [Theory]
    [SpecialityAutoData]
    public void AddSubject_ShouldAddSubject_WhenAllParametersAreValidAndSubjectNotExists(Speciality speciality)
    {
        // Arrange
        var id = new SubjectId(Guid.NewGuid());
        var title = "subject_title";
        var teacherId = new TeacherId(Guid.NewGuid());

        // Act
        var subject = speciality.AddSubject(id, title, teacherId);

        // Assert
        subject.Should().NotBeNull();
        subject.Id.Should().Be(id);
        subject.SpecialityId.Should().Be(speciality.Id);
        subject.Title.Should().Be(title);
        subject.TeacherId.Should().Be(teacherId);
        speciality.Subjects.Should().Contain(subject);
        speciality.DomainEvents.Should().ContainEquivalentOf(new SubjectAddedToSpecialityEvent(speciality, subject));
    }

    [Theory]
    [SpecialityMemberAutoData(nameof(GetSubjectDefaultParameters))]
    public void AddSubject_ShouldThrowArgumentException_WhenParametersAreDefault(
        SubjectId id, string title, TeacherId teacherId, Speciality speciality)
    {
        // Act
        var act = () => speciality.AddSubject(id, title, teacherId);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [SpecialityAutoData]
    public void AddSubject_ShouldThrowSubjectAlreadyExistsException_WhenSubjectAlreadyExists(Speciality speciality)
    {
        // Arrange
        var id = new SubjectId(Guid.NewGuid());
        var title = "subject_title";
        var teacherId = new TeacherId(Guid.NewGuid());
        var subject = speciality.AddSubject(id, title, teacherId);

        // Act
        var act = () => speciality.AddSubject(id, title, teacherId);

        // Assert
        act.Should().Throw<SubjectAlreadyExistsException>();
    }

    [Theory]
    [SpecialityAutoData]
    public void RemoveSubject_ShouldRemoveSubject_WhenSubjectExists(Speciality speciality)
    {
        // Arrange
        var id = new SubjectId(Guid.NewGuid());
        var title = "subject_title";
        var teacherId = new TeacherId(Guid.NewGuid());
        var subject = speciality.AddSubject(id, title, teacherId);

        // Act
        speciality.RemoveSubject(id);

        // Assert
        speciality.Subjects.Should().NotContain(subject);
        speciality.DomainEvents.Should().ContainEquivalentOf(new SubjectRemovedFromSpecialityEvent(speciality, subject));
    }

    [Theory]
    [SpecialityAutoData]
    public void RemoveSubject_ShouldThrowSubjectNotFoundException_WhenSubjectNotExists(Speciality speciality)
    {
        // Act
        var act = () => speciality.RemoveSubject(default);

        // Assert
        act.Should().Throw<SubjectNotFoundException>();
    }

    private static TheoryData<GroupId, YearOfAdmission> GetGroupDefaultParameters()
    {
        var id = new GroupId(Guid.NewGuid());
        var yearOfAdmission = new YearOfAdmission(2022);

        return new TheoryData<GroupId, YearOfAdmission>
            {
                { default!, yearOfAdmission},
                { id, default!}
            };
    }

    private static TheoryData<SubjectId, string, TeacherId> GetSubjectDefaultParameters()
    {
        var id = new SubjectId(Guid.NewGuid());
        var title = "subject_title";
        var teacherId = new TeacherId(Guid.NewGuid());

        return new TheoryData<SubjectId, string, TeacherId>
            {
                { default!, title, teacherId },
                { id, default!, teacherId },
                { id, "", teacherId },
                { id, "  ", teacherId },
                { id, title, default! },
            };
    }
}

public class SpecialityMemberAutoData : MemberAutoDataAttribute
{
    public SpecialityMemberAutoData(string memberName, params object[] parameters)
        : base(new SpecialityAutoData(), memberName, parameters) { }
}

public class SpecialityAutoData : AutoDataAttribute
{
    public SpecialityAutoData() : base(Create) { }

    private static IFixture Create()
    {
        var fixture = new Fixture();

        var id = new SpecialityId(Guid.NewGuid());
        var title = "Информатика и Вычислительная Техника";
        var attendanceType = AttendanceType.Attended;
        var degree = Degree.Baccalaureate;
        var yearOfAdmission = new YearOfAdmission(2022);

        fixture.Inject(yearOfAdmission);
        fixture.Register(() => new Speciality(id, title, attendanceType, degree));

        return fixture;
    }
}
