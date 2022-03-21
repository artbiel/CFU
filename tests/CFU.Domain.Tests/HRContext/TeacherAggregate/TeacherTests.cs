using CFU.Domain.HRContext;
using CFU.Domain.HRContext.DepartmentAggregate;
using CFU.Domain.HRContext.TeacherAggregate;

namespace CFU.Domain.UnitTests.HRContext.TeacherAggregate;

public class TeacherTests
{
    [Fact]
    public void Constructor_ShouldCreateTeacher_WhenAllParametersAreValid()
    {
        // Arrange
        var id = new TeacherId(Guid.NewGuid());
        var department = new Department(new DepartmentId(Guid.NewGuid()));
        var surname = "surname";
        var name = "name";
        var patronymic = "patronymic";

        // Act
        var teacher = new Teacher(id, department, surname, name, patronymic);

        // Assert
        teacher.Should().NotBeNull();
        teacher.Id.Should().Be(id);
        teacher.DepartmentId.Should().Be(department.Id);
        teacher.Surname.Should().Be(surname);
        teacher.Name.Should().Be(name);
        teacher.Patronymic.Should().Be(patronymic);
        teacher.DomainEvents.Should().ContainEquivalentOf(new TeacherAssignedToDepartmentEvent(teacher, department.Id));
    }

    [Theory]
    [MemberData(nameof(GetTeacherDefaultParameters))]
    public void Constructor_ShouldThrowArgumentException_WhenParametersAreDefault(
        TeacherId id, Department department, string surname, string name, string patronymic)
    {
        // Act
        var act = () => new Teacher(id, department, surname, name, patronymic);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [TeacherAutoData]
    public void SetSurname_ShouldSetSurname_WhenSurnameIsValid(Teacher teacher, string surname)
    {
        // Act
        teacher.SetSurname(surname);

        // Assert
        teacher.Surname.Should().Be(surname);
    }

    [Theory]
    [TeacherInlineAutoData(default!)]
    [TeacherInlineAutoData("")]
    [TeacherInlineAutoData("  ")]
    public void SetSurname_ShouldThrowArgumentException_WhenSurnameIsInvalid(string surname, Teacher teacher)
    {
        // Act
        var act = () => teacher.SetSurname(surname);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [TeacherAutoData]
    public void SetName_ShouldSetName_WhenNameIsValid(Teacher teacher, string name)
    {
        // Act
        teacher.SetName(name);

        // Assert
        teacher.Name.Should().Be(name);
    }

    [Theory]
    [TeacherInlineAutoData(default!)]
    [TeacherInlineAutoData("")]
    [TeacherInlineAutoData("  ")]
    public void SetName_ShouldThrowArgumentException_WhenNameIsInvalid(string name, Teacher teacher)
    {
        // Act
        var act = () => teacher.SetName(name);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [TeacherAutoData]
    public void SetPatronymic_ShouldSetPatronymic_WhenPatronymicIsValid(Teacher teacher, string patronymic)
    {
        // Act
        teacher.SetPatronymic(patronymic);

        // Assert
        teacher.Patronymic.Should().Be(patronymic);
    }

    [Theory]
    [TeacherInlineAutoData(default!)]
    [TeacherInlineAutoData("")]
    [TeacherInlineAutoData("  ")]
    public void SetPatronymic_ShouldThrowArgumentException_WhenPatronymicIsInvalid(string patronymic, Teacher teacher)
    {
        // Act
        var act = () => teacher.SetPatronymic(patronymic);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [TeacherAutoData]
    public void SetEmail_ShouldSetEmail_WhenEmailIsValid(Teacher teacher, Email email)
    {
        // Act
        teacher.SetEmail(email);

        // Assert
        teacher.Email.Should().Be(email);
    }

    [Theory]
    [TeacherAutoData]
    public void SetEmail_ShouldThrowArgumentException_WhenEmailIsDefault(Teacher teacher)
    {
        // Act
        var act = () => teacher.SetEmail(default);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [TeacherAutoData]
    public void SetPhoneNumber_ShouldSetPhoneNumber_WhenPhoneNumberIsValid(Teacher teacher, PhoneNumber phoneNumber)
    {
        // Act
        teacher.SetPhoneNumber(phoneNumber);

        // Assert
        teacher.PhoneNumber.Should().Be(phoneNumber);
    }

    [Theory]
    [TeacherAutoData]
    public void SetPhoneNumber_ShouldThrowArgumentException_WhenPhoneNumberIsDefault(Teacher teacher)
    {
        // Act
        var act = () => teacher.SetPhoneNumber(default);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [TeacherAutoData]
    public void AssingToDepartment_ShouldAssingToDepartment_WhenDepartmentIsValid(Teacher teacher)
    {
        // Arrange
        var newDepartment = new Department(new DepartmentId(Guid.NewGuid()));

        // Act
        teacher.AssingToDepartment(newDepartment);

        // Assert
        teacher.DepartmentId.Should().Be(newDepartment.Id);
        teacher.DomainEvents.Should().ContainEquivalentOf(new TeacherAssignedToDepartmentEvent(teacher, newDepartment.Id));
    }

    [Theory]
    [TeacherAutoData]
    public void AssingToDepartment_ShouldThrowArgumentException_WhenDepartmentIsDefault(Teacher teacher)
    {
        // Act
        var act = () => teacher.AssingToDepartment(default);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    private static TheoryData<TeacherId, Department, string, string, string> GetTeacherDefaultParameters()
    {
        var id = new TeacherId(Guid.NewGuid());
        var department = new Department(new DepartmentId(Guid.NewGuid()));
        var surname = "surname";
        var name = "name";
        var patronymic = "patronymic";

        return new TheoryData<TeacherId, Department, string, string, string>
            {
                { default!, department, surname, name, patronymic },
                { id, default!, surname, name, patronymic },
                { id, department, default!, name, patronymic },
                { id, department, "", name, patronymic },
                { id, department, "  ", name, patronymic },
                { id, department, surname, default!,  patronymic },
                { id, department, surname, "", patronymic },
                { id, department, surname, "  ",  patronymic },
                { id, department, surname, name,  default! },
                { id, department, surname, name, "" },
                { id, department, surname, name,  "  " }
            };
    }
}

public class TeacherInlineAutoData : InlineAutoDataAttribute
{
    public TeacherInlineAutoData(params object[] values) : base(new TeacherAutoData(), values) { }
}

public class TeacherAutoData : AutoDataAttribute
{
    public TeacherAutoData() : base(Create) { }

    private static IFixture Create()
    {
        var fixture = new Fixture();

        var id = new TeacherId(Guid.NewGuid());
        var department = new Department(new DepartmentId(Guid.NewGuid()));
        var surname = "surname";
        var name = "name";
        var patronymic = "patronymic";
        var email = new Email("example@mail.ru");
        var phoneNumber = new PhoneNumber("+79788888888");

        fixture.Inject(email);
        fixture.Inject(phoneNumber);
        fixture.Register(() => new Teacher(id, department, surname, name, patronymic));

        return fixture;
    }
}
