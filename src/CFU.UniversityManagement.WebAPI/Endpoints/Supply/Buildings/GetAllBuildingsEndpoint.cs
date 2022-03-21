using CFU.UniversityManagement.Application.Supply.DTOs;
using CFU.UniversityManagement.Application.Supply.Queries;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Supply.Building;

public class GetAllBuildingsEndpoint : EndpointWithoutRequest<GetAllBuildingsResponse>
{
    private readonly IMediator _mediator;

    public GetAllBuildingsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/building");
        AllowAnonymous();
    }

    public async override Task HandleAsync(CancellationToken ct)
    {
        var buildingsDTO = await _mediator.Send(new GetAllBuildingsQuery(), ct);
        await SendAsync(new(buildingsDTO), cancellation: ct);
    }
}

public record struct GetAllBuildingsResponse(IEnumerable<BuildingDTO> Buildings);
