using CFU.Domain.StructureContext.FacultyAggregate;

namespace CFU.UniversityManagement.Application.Structure.Commands;

public record DisbandFacultyCommand(FacultyId Id) : ICommand<OneOf<Success, NotFound>>;

public class DisbandFacultyCommandValidator : AbstractValidator<DisbandFacultyCommand>
{
    public DisbandFacultyCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}

public class DisbandFacultyCommandHandler : IRequestHandler<DisbandFacultyCommand, OneOf<Success, NotFound>>
{
    private readonly IFacultyRepository _repository;

    public DisbandFacultyCommandHandler(IFacultyRepository repository)
    {
        _repository = Guard.Against.Default(repository, nameof(repository));
    }

    public async Task<OneOf<Success, NotFound>> Handle(DisbandFacultyCommand request, CancellationToken cancellationToken)
    {
        var faculty = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (faculty is null)
            return new NotFound();
        faculty.Disband();
        await _repository.Update(faculty);
        return new Success();
    }
}
