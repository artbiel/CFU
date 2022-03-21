using CFU.Domain.StructureContext.InstituteAggregate;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.Application.Structure.Commands;

public record CreateInstituteDepartmentCommand(InstituteId InstituteId, string Title) : ICommand<OneOf<Success<DepartmentDTO>, NotFound>>;

public class CreateInstituteDepartmentCommandValidator : AbstractValidator<CreateInstituteDepartmentCommand>
{
    public CreateInstituteDepartmentCommandValidator()
    {
        RuleFor(v => v.InstituteId).NotEmpty();
        RuleFor(v => v.Title).MaximumLength(200).NotEmpty();
    }
}

public class CreateInstituteDepartmentCommandHandler : IRequestHandler<CreateInstituteDepartmentCommand, OneOf<Success<DepartmentDTO>, NotFound>>
{
    private readonly IInstituteRepository _repository;
    private readonly IMapper _mapper;

    public CreateInstituteDepartmentCommandHandler(IInstituteRepository instituteRepository, IMapper mapper)
    {
        _repository = Guard.Against.Default(instituteRepository, nameof(instituteRepository));
        _mapper = Guard.Against.Default(mapper, nameof(mapper));
    }

    public async Task<OneOf<Success<DepartmentDTO>, NotFound>> Handle(CreateInstituteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var departmentId = new DepartmentId(Guid.NewGuid());
        var institute = await _repository.GetByIdAsync(request.InstituteId, cancellationToken);
        if (institute is null)
            return new NotFound();
        var department = institute.CreateDepartment(departmentId, request.Title);
        await _repository.Update(institute);
        var departmentDTO = _mapper.Map<Department, DepartmentDTO>(department);
        return new Success<DepartmentDTO>(departmentDTO);
    }
}
