using CFU.Domain.SupplyContext.BuildingAggregate;

namespace CFU.UniversityManagement.Application.Supply.Commands;

public record DecommissionBuildingCommand(BuildingId Id) : ICommand<OneOf<Success, NotFound>>;

public class DecommissionBuildingCommandValidator : AbstractValidator<DecommissionBuildingCommand>
{
    public DecommissionBuildingCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}

public class DecommissionBuildingCommandHandler : IRequestHandler<DecommissionBuildingCommand, OneOf<Success, NotFound>>
{
    private readonly IBuildingRepository _repository;

    public DecommissionBuildingCommandHandler(IBuildingRepository repository)
    {
        _repository = Guard.Against.Default(repository, nameof(repository));
    }

    public async Task<OneOf<Success, NotFound>> Handle(DecommissionBuildingCommand request, CancellationToken cancellationToken)
    {
        var building = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (building is null)
            return new NotFound();
        building.Decommission();
        return new Success();
    }
}
