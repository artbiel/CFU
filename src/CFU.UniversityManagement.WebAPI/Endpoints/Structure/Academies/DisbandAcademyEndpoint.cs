using CFU.UniversityManagement.Application.Structure.Commands;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Structure.Academies;

public class DisbandAcademyEndpoint : Endpoint<DisbandAcademyEndpointRequest>
{
    private readonly IMediator _mediator;

    public DisbandAcademyEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete(StructureRoutes.AcademyById);
        AllowAnonymous();
    }

    public async override Task HandleAsync(DisbandAcademyEndpointRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new DisbandAcademyCommand(new AcademyId(req.Id)), ct);
        await result.Match(success => SendOkAsync(cancellation: ct), notFound => SendNotFoundAsync(ct));
    }
}

public record DisbandAcademyEndpointRequest
{
    public Guid Id { get; set; }
}
