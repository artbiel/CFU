using CFU.Domain.StructureContext.AcademyAggregate;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.Application.Structure.Commands;

public record CreateFacultyCommand(AcademyId AcademyId, string Title) : ICommand<OneOf<Success<FacultyDTO>, NotFound>>;

public class CreateFacultyCommandValidator : AbstractValidator<CreateFacultyCommand>
{
    public CreateFacultyCommandValidator()
    {
        RuleFor(v => v.AcademyId).NotEmpty();
        RuleFor(v => v.Title).MaximumLength(200).NotEmpty();
    }
}

public class CreateFacultyCommandHandler : IRequestHandler<CreateFacultyCommand, OneOf<Success<FacultyDTO>, NotFound>>
{
    private readonly IAcademyRepository _repository;
    private readonly IMapper _mapper;

    public CreateFacultyCommandHandler(IAcademyRepository academyRepository, IMapper mapper)
    {
        _repository = Guard.Against.Default(academyRepository, nameof(academyRepository));
        _mapper = Guard.Against.Default(mapper, nameof(mapper));
    }

    public async Task<OneOf<Success<FacultyDTO>, NotFound>> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
    {
        var faulctyId = new FacultyId(Guid.NewGuid());
        var academy = await _repository.GetByIdAsync(request.AcademyId, cancellationToken);
        if (academy is null)
            return new NotFound();
        var faculty = academy.CreateFaculty(faulctyId, request.Title);
        await _repository.Update(academy);
        var facultyDTO = _mapper.Map<Faculty, FacultyDTO>(faculty);
        return new Success<FacultyDTO>(facultyDTO);
    }
}
