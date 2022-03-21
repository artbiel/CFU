using CFU.Domain.SupplyContext.BuildingAggregate;
using CFU.UniversityManagement.Application.Supply.DTOs;

namespace CFU.UniversityManagement.Application.Supply.Commands;

public record CreateBuildingCommand(string City, string Street, int Number, string Block) : ICommand<BuildingDTO>;

public class CreateBuildingCommandValidator : AbstractValidator<CreateBuildingCommand>
{
    public CreateBuildingCommandValidator()
    {
        RuleFor(v => v.City).MaximumLength(50).NotEmpty();
        RuleFor(v => v.Street).MaximumLength(50).NotEmpty();
        RuleFor(v => v.Number).GreaterThanOrEqualTo(1);
        RuleFor(v => v.Block).MaximumLength(30).NotEmpty();
    }
}

public class CreateBuildingCommandHandler : IRequestHandler<CreateBuildingCommand, BuildingDTO>
{
    private readonly BuildingFactory _factory;
    private readonly IBuildingRepository _repository;
    private readonly IMapper _mapper;

    public CreateBuildingCommandHandler(BuildingFactory factory, IBuildingRepository repository, IMapper mapper)
    {
        _factory = Guard.Against.Default(factory, nameof(factory));
        _repository = Guard.Against.Default(repository, nameof(repository));
        _mapper = Guard.Against.Default(mapper, nameof(mapper));
    }

    public async Task<BuildingDTO> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
    {
        var id = new BuildingId(Guid.NewGuid());
        var address = new Address(request.City,
            request.Street,
            new BuildingNumber(request.Number),
            new BuildingBlock(request.Block));
        var building = await _factory.CreateAsync(id, address, cancellationToken);
        _repository.Add(building);
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<Building, BuildingDTO>(building);
    }
}
