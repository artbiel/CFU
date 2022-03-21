using CFU.UniversityManagement.Application.Structure.Commands;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Structure.Academies;

public class CreateAcademyEndpoint : Endpoint<CreateAcademyEndpointRequest, CreateAcademyEndpointResponse>
{
    private readonly IMediator _mediator;

    public CreateAcademyEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(StructureRoutes.Academy);
        AllowAnonymous();
    }

    public async override Task HandleAsync(CreateAcademyEndpointRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateAcademyCommand(req.Title), ct);

        await SendAsync(new CreateAcademyEndpointResponse(result), cancellation: ct);
    }
}

public record struct CreateAcademyEndpointRequest(string Title);
public record struct CreateAcademyEndpointResponse(AcademyDTO Academy);
