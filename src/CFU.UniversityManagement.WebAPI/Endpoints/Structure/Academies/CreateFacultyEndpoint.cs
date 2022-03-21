using CFU.UniversityManagement.Application.Structure.Commands;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Structure.Faculties;

public class CreateFacultyEndpoint : Endpoint<CreateFacultyEndpointRequest, CreateFacultyEndpointResponse>
{
    private readonly IMediator _mediator;

    public CreateFacultyEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(StructureRoutes.AcademyFaculties);
        AllowAnonymous();
    }

    public async override Task HandleAsync(CreateFacultyEndpointRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateFacultyCommand(new AcademyId(req.AcademyId), req.Title), ct);
        await result.Match(success => SendAsync(new(success.Value), cancellation: ct), notFound => SendNotFoundAsync(ct));
    }
}

public record CreateFacultyEndpointRequest
{
    public Guid AcademyId { get; set; }
    public string? Title { get; set; }
}

public record struct CreateFacultyEndpointResponse(FacultyDTO Faculty);
