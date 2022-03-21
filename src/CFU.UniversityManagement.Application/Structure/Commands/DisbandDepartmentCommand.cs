using CFU.Domain.StructureContext.DepartmentAggregate;

namespace CFU.UniversityManagement.Application.Structure.Commands;

public record DisbandDepartmentCommand(DepartmentId Id) : ICommand<OneOf<Success, NotFound>>;

public class DisbandDepartmentCommandValidator : AbstractValidator<DisbandDepartmentCommand>
{
    public DisbandDepartmentCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}

public class DisbandFacultyDepartmentCommandHandler : IRequestHandler<DisbandDepartmentCommand, OneOf<Success, NotFound>>
{
    private readonly IDepartmentRepository _repository;

    public DisbandFacultyDepartmentCommandHandler(IDepartmentRepository repository)
    {
        _repository = Guard.Against.Default(repository, nameof(repository));
    }

    public async Task<OneOf<Success, NotFound>> Handle(DisbandDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (department is null)
            return new NotFound();
        department.Disband();
        await _repository.Update(department);
        return new Success();
    }
}
