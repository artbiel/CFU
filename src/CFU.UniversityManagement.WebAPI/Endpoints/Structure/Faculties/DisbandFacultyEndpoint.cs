using CFU.UniversityManagement.Application.Structure.Commands;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Structure.Faculties;

public class DisbandFacultyEndpoint : Endpoint<DisbandFacultyEndpointRequest>
{
    private readonly IMediator _mediator;

    public DisbandFacultyEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete(StructureRoutes.FacultyById);
        AllowAnonymous();
    }

    public async override  Task HandleAsync(DisbandFacultyEndpointRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new DisbandFacultyCommand(new FacultyId(req.Id)), ct);
        await result.Match(success => SendOkAsync(cancellation: ct), notFound => SendNotFoundAsync(ct));
    }
}

public record DisbandFacultyEndpointRequest
{
    public Guid Id { get; set; }
}
