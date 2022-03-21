using CFU.Domain.StructureContext.InstituteAggregate;
using System.Threading.Tasks;

namespace CFU.Domain.UnitTests.StructureContext.InstituteAggregate;

public class InstituteFactoryTests
{
    [Fact]
    public void Constructor_ShouldCreateInstituteFactoryFactory_WhenAllParametersAreValid()
    {
        // Arrange
        var repository = Substitute.For<IInstituteRepository>();

        // Act
        var factory = new InstituteFactory(repository);

        // Assert
        factory.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenParametersAreDefault()
    {
        // Act
        var act = () => new InstituteFactory(default);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory, AutoData]
    public async Task Create_ShouldCreatePairsTimetable_WhenAllParametersAreValid(InstituteId id, string title)
    {
        // Arrange
        var repository = Substitute.For<IInstituteRepository>();
        var factory = new InstituteFactory(repository);
        Institute? existingInstitute = default;
        repository.GetAsync(Arg.Is(new UniqueInstituteSpecification(new Institute(id, title)))).Returns(existingInstitute);

        // Act
        var institute = await factory.CreateAsync(id, title);

        // Assert
        institute.Should().NotBeNull();
        institute.Id.Should().Be(id);
        institute.Title.Should().Be(title);
        institute.Departments.Should().BeEmpty();
        institute.IsDisbanded.Should().BeFalse();
        institute.DomainEvents.Should().ContainSingle();
        institute.DomainEvents.Should().ContainEquivalentOf(new InstituteCreatedEvent(institute));
    }

    [Theory]
    [MemberData(nameof(GetInstituteDefaultParameters))]
    public async Task Create_ShouldThrowArgumentException_WhenParametersAreDefault(
        InstituteId id,
        string title)
    {
        var repository = Substitute.For<IInstituteRepository>();
        var factory = new InstituteFactory(repository);

        // Act
        var act = () => factory.CreateAsync(id, title);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Theory, AutoData]
    public async Task Create_ShouldThrowPairsTimetableAlreadyExistsException_WhenPairsTimetableAlreadyExists
        (InstituteId id, string title)
    {
        // Arrange
        var existingInstitute = new Institute(id, title);
        var repository = Substitute.For<IInstituteRepository>();
        var factory = new InstituteFactory(repository);
        repository.GetAsync(Arg.Is(new UniqueInstituteSpecification(new Institute(id, title)))).Returns(existingInstitute);

        // Act
        var act = () => factory.CreateAsync(id, title);

        // Assert
        await act.Should().ThrowAsync<InstituteAlreadyExistException>();
    }

    public static TheoryData<InstituteId, string> GetInstituteDefaultParameters()
    {
        var id = new InstituteId(Guid.NewGuid());
        var title = "title";

        return new TheoryData<InstituteId, string>
            {
                { default!, title},
                { id, default!}
            };
    }
}
