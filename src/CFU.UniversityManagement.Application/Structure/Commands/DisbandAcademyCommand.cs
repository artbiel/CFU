using CFU.Domain.StructureContext.AcademyAggregate;

namespace CFU.UniversityManagement.Application.Structure.Commands;

public record DisbandAcademyCommand(AcademyId Id) : ICommand<OneOf<Success, NotFound>>;

public class DisbandAcademyCommandValidator : AbstractValidator<DisbandAcademyCommand>
{
    public DisbandAcademyCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}

public class DisbandAcademyCommandHandler : IRequestHandler<DisbandAcademyCommand, OneOf<Success, NotFound>>
{
    private readonly IAcademyRepository _repository;

    public DisbandAcademyCommandHandler(IAcademyRepository academyRepository)
    {
        _repository = Guard.Against.Default(academyRepository, nameof(academyRepository));
    }

    public async Task<OneOf<Success, NotFound>> Handle(DisbandAcademyCommand request, CancellationToken cancellationToken)
    {
        var academy = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (academy is null)
            return new NotFound();
        await _repository.Update(academy);
        academy.Disband();
        return new Success();
    }
}
