using CFU.UniversityManagement.Application.Structure.DTOs;
using CFU.UniversityManagement.Application.StructureContext.Queries;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Structure.Academies;

public class GetUniversityStructureEndpoint : EndpointWithoutRequest<GetUniversityStructureResponse>
{
    private readonly IMediator _mediator;

    public GetUniversityStructureEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(StructureRoutes.Structure);
        AllowAnonymous();
    }

    public async override Task HandleAsync(CancellationToken ct)
    {
        var structureDTO = await _mediator.Send(new GetUniversityStructureQuery(), ct);
        await SendAsync(new(structureDTO), cancellation: ct);
    }
}

public record struct GetUniversityStructureResponse(UniversityStructureDTO UniversityStructure);
