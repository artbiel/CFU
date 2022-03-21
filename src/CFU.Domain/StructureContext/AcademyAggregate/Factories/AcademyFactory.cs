using System.Threading;

namespace CFU.Domain.StructureContext.AcademyAggregate;

public class AcademyFactory
{
    private readonly IAcademyRepository _repository;

    public AcademyFactory(IAcademyRepository repository)
    {
        _repository = Guard.Against.Default(repository, nameof(repository));
    }

    public async Task<Academy> CreateAsync(
        AcademyId id,
        string title,
        CancellationToken cancellationToken = default)
    {
        var academy = new Academy(id, title);

        var existingAcademy = await _repository.GetAsync(new UniqueAcademySpecification(academy), cancellationToken);
        if (existingAcademy is not null)
            throw new AcademyAlreadyExistException(academy);

        academy.AddDomainEvent(new AcademyCreatedEvent(academy));
        return academy;
    }
}
