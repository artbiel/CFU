using CFU.UniversityManagement.Application.Structure.Commands;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Structure.Institutes;

public class CreateInstituteEndpoint : Endpoint<CreateInstituteEndpointRequest, CreateInstituteEndpointResponse>
{
    private readonly IMediator _mediator;

    public CreateInstituteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(StructureRoutes.Institute);
        AllowAnonymous();
    }

    public async override Task HandleAsync(CreateInstituteEndpointRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateInstituteCommand(req.Title), ct);


        await SendAsync(new CreateInstituteEndpointResponse(result), cancellation: ct);
    }
}

public record struct CreateInstituteEndpointRequest(string Title);
public record struct CreateInstituteEndpointResponse(InstituteDTO Institute);
