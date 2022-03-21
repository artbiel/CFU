using System.Threading;

namespace CFU.Domain.StructureContext.InstituteAggregate;

public class InstituteFactory
{
    private readonly IInstituteRepository _repository;

    public InstituteFactory(IInstituteRepository repository)
    {
        _repository = Guard.Against.Default(repository, nameof(repository));
    }

    public async Task<Institute> CreateAsync(
        InstituteId id,
        string title,
        CancellationToken cancellationToken = default)
    {
        var institute = new Institute(id, title);

        var existingInstitute = await _repository.GetAsync(new UniqueInstituteSpecification(institute), cancellationToken);
        if (existingInstitute is not null)
            throw new InstituteAlreadyExistException(institute);

        institute.AddDomainEvent(new InstituteCreatedEvent(institute));

        return institute;
    }
}
