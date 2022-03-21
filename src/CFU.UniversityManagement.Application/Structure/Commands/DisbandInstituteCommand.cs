using CFU.Domain.StructureContext.InstituteAggregate;

namespace CFU.UniversityManagement.Application.Structure.Commands;

public record DisbandInstituteCommand(InstituteId Id) : ICommand<OneOf<Success, NotFound>>;

public class DisbandInstituteCommandValidator : AbstractValidator<DisbandInstituteCommand>
{
    public DisbandInstituteCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}

public class DisbandInstituteCommandHandler : IRequestHandler<DisbandInstituteCommand, OneOf<Success, NotFound>>
{
    private readonly IInstituteRepository _repository;

    public DisbandInstituteCommandHandler(IInstituteRepository InstituteRepository)
    {
        _repository = Guard.Against.Default(InstituteRepository, nameof(InstituteRepository));
    }

    public async Task<OneOf<Success, NotFound>> Handle(DisbandInstituteCommand request, CancellationToken cancellationToken)
    {
        var institute = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (institute is null)
            return new NotFound();
        institute.Disband();
        await _repository.Update(institute);
        return new Success();
    }
}
