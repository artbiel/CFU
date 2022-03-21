using CFU.Domain.StructureContext.AcademyAggregate;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.Application.Structure.Commands;

public record CreateAcademyCommand(string Title) : ICommand<AcademyDTO>;

public class CreateAcademyCommandValidator : AbstractValidator<CreateAcademyCommand>
{
    public CreateAcademyCommandValidator()
    {
        RuleFor(v => v.Title).MaximumLength(200).NotEmpty();
    }
}

public class CreateAcademyCommandHandler : IRequestHandler<CreateAcademyCommand, AcademyDTO>
{
    private readonly AcademyFactory _factory;
    private readonly IAcademyRepository _repository;
    private readonly IMapper _mapper;

    public CreateAcademyCommandHandler(AcademyFactory factory, IAcademyRepository academyRepository, IMapper mapper)
    {
        _factory = Guard.Against.Default(factory, nameof(factory));
        _repository = Guard.Against.Default(academyRepository, nameof(academyRepository));
        _mapper = Guard.Against.Default(mapper, nameof(mapper));
    }

    public async Task<AcademyDTO> Handle(CreateAcademyCommand request, CancellationToken cancellationToken)
    {
        var id = new AcademyId(Guid.NewGuid());
        var academy = await _factory.CreateAsync(id, request.Title, cancellationToken);
        await _repository.Add(academy);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<Academy, AcademyDTO>(academy);
    }
}
