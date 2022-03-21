using CFU.Domain.StructureContext.FacultyAggregate;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.Application.Structure.Commands;

public record CreateFacultyDepartmentCommand(FacultyId FacultyId, string Title) : ICommand<OneOf<Success<DepartmentDTO>, NotFound>>;

public class CreateFacultyDepartmentCommandValidator : AbstractValidator<CreateFacultyDepartmentCommand>
{
    public CreateFacultyDepartmentCommandValidator()
    {
        RuleFor(v => v.FacultyId).NotEmpty();
        RuleFor(v => v.Title).MaximumLength(200).NotEmpty();
    }
}

public class CreateFacultyDepartmentCommandHandler : IRequestHandler<CreateFacultyDepartmentCommand, OneOf<Success<DepartmentDTO>, NotFound>>
{
    private readonly IFacultyRepository _repository;
    private readonly IMapper _mapper;

    public CreateFacultyDepartmentCommandHandler(IFacultyRepository facultyRepository, IMapper mapper)
    {
        _repository = Guard.Against.Default(facultyRepository, nameof(facultyRepository));
        _mapper = Guard.Against.Default(mapper, nameof(mapper));
    }

    public async Task<OneOf<Success<DepartmentDTO>, NotFound>> Handle(CreateFacultyDepartmentCommand request, CancellationToken cancellationToken)
    {
        var departmentId = new DepartmentId(Guid.NewGuid());
        var faculty = await _repository.GetByIdAsync(request.FacultyId, cancellationToken);
        if (faculty is null)
            return new NotFound();
        var department = faculty.CreateDepartment(departmentId, request.Title);
        await _repository.Update(faculty);
        var departmentDTO = _mapper.Map<Department, DepartmentDTO>(department);
        return new Success<DepartmentDTO>(departmentDTO);
    }
}
