using CFU.UniversityManagement.Application.Supply.Commands;
using CFU.UniversityManagement.Application.Supply.DTOs;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Supply.Building;

public class CreateBuildingEndpoint : Endpoint<CreateBuildingEndpointRequest, CreateBuildingEndpointResponse>
{
    private readonly IMediator _mediator;

    public CreateBuildingEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/building");
        AllowAnonymous();
    }

    public async override Task HandleAsync(CreateBuildingEndpointRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateBuildingCommand(req.City, req.Street, req.Number, req.Block), ct);

        await SendAsync(new CreateBuildingEndpointResponse(result), cancellation: ct);
    }
}

public record struct CreateBuildingEndpointRequest(string City, string Street, int Number, string Block);
public record struct CreateBuildingEndpointResponse(BuildingDTO Building);
