using CFU.UniversityManagement.Application.Supply.Commands;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Supply.Building;

public class DecommissionBuildingEndpoint : Endpoint<DecommissionBuildingEndpointRequest>
{
    private readonly IMediator _mediator;

    public DecommissionBuildingEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("/building");
        AllowAnonymous();
    }

    public async override Task HandleAsync(DecommissionBuildingEndpointRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new DecommissionBuildingCommand(new BuildingId(req.Id)), ct);
        await result.Match(success => SendOkAsync(), notFound => SendNotFoundAsync());
    }
}

public struct DecommissionBuildingEndpointRequest
{
    public Guid Id { get; set; }
}
