using CFU.Domain.StructureContext.InstituteAggregate;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.Application.Structure.Commands;

public record CreateInstituteCommand(string Title) : ICommand<InstituteDTO>;

public class CreateInstituteCommandValidator : AbstractValidator<CreateInstituteCommand>
{
    public CreateInstituteCommandValidator()
    {
        RuleFor(v => v.Title).MaximumLength(200).NotEmpty();
    }
}

public class CreateInstituteCommandHandler : IRequestHandler<CreateInstituteCommand, InstituteDTO>
{
    private readonly InstituteFactory _factory;
    private readonly IInstituteRepository _repository;
    private readonly IMapper _mapper;

    public CreateInstituteCommandHandler(InstituteFactory factory, IInstituteRepository instituteRepository, IMapper mapper)
    {
        _factory = Guard.Against.Default(factory, nameof(factory));
        _repository = Guard.Against.Default(instituteRepository, nameof(instituteRepository));
        _mapper = Guard.Against.Default(mapper, nameof(mapper));
    }

    public async Task<InstituteDTO> Handle(CreateInstituteCommand request, CancellationToken cancellationToken)
    {
        var id = new InstituteId(Guid.NewGuid());
        var institute = await _factory.CreateAsync(id, request.Title, cancellationToken);
        await _repository.Add(institute);
        return _mapper.Map<Institute, InstituteDTO>(institute);
    }
}
