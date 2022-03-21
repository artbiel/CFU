using CFU.Domain.StructureContext.AcademyAggregate;
using System.Threading.Tasks;

namespace CFU.Domain.UnitTests.StructureContext.AcademyAggregate;

public class AcademyFactoryTests
{
    [Fact]
    public void Constructor_ShouldCreateAcademyFactoryFactory_WhenAllParametersAreValid()
    {
        // Arrange
        var repository = Substitute.For<IAcademyRepository>();

        // Act
        var factory = new AcademyFactory(repository);

        // Assert
        factory.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenParametersAreDefault()
    {
        // Act
        var act = () => new AcademyFactory(default);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory, AutoData]
    public async Task Create_ShouldCreatePairsTimetable_WhenAllParametersAreValid(AcademyId id, string title)
    {
        // Arrange
        var repository = Substitute.For<IAcademyRepository>();
        var factory = new AcademyFactory(repository);
        Academy? existingAcademy = default;
        repository.GetAsync(Arg.Is(new UniqueAcademySpecification(new Academy(id, title)))).Returns(existingAcademy);

        // Act
        var academy = await factory.CreateAsync(id, title);

        // Assert
        academy.Should().NotBeNull();
        academy.Id.Should().Be(id);
        academy.Title.Should().Be(title);
        academy.Faculties.Should().BeEmpty();
        academy.IsDisbanded.Should().BeFalse();
        academy.DomainEvents.Should().ContainSingle();
        academy.DomainEvents.Should().ContainEquivalentOf(new AcademyCreatedEvent(academy));
    }

    [Theory]
    [MemberData(nameof(GetAcademyDefaultParameters))]
    public async Task Create_ShouldThrowArgumentException_WhenParametersAreDefault(
        AcademyId id,
        string title)
    {
        var repository = Substitute.For<IAcademyRepository>();
        var factory = new AcademyFactory(repository);

        // Act
        var act = () => factory.CreateAsync(id, title);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Theory, AutoData]
    public async Task Create_ShouldThrowPairsTimetableAlreadyExistsException_WhenPairsTimetableAlreadyExists
        (AcademyId id, string title)
    {
        // Arrange
        var existingAcademy = new Academy(id, title);
        var repository = Substitute.For<IAcademyRepository>();
        var factory = new AcademyFactory(repository);
        repository.GetAsync(Arg.Is(new UniqueAcademySpecification(new Academy(id, title)))).Returns(existingAcademy);

        // Act
        var act = () => factory.CreateAsync(id, title);

        // Assert
        await act.Should().ThrowAsync<AcademyAlreadyExistException>();
    }

    public static TheoryData<AcademyId, string> GetAcademyDefaultParameters()
    {
        var id = new AcademyId(Guid.NewGuid());
        var title = "title";

        return new TheoryData<AcademyId, string>
            {
                { default!, title},
                { id, default!}
            };
    }
}
