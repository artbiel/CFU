using CFU.UniversityManagement.Application.Structure.Commands;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Structure.Academies;

public class DisbandInstituteEndpoint : Endpoint<DisbandInstituteEndpointRequest>
{
    private readonly IMediator _mediator;

    public DisbandInstituteEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete(StructureRoutes.InstituteById);
        AllowAnonymous();
    }

    public async override Task HandleAsync(DisbandInstituteEndpointRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new DisbandInstituteCommand(new InstituteId(req.Id)), ct);

        await result.Match(success => SendOkAsync(cancellation: ct), notFound => SendNotFoundAsync(ct));
    }
}

public struct DisbandInstituteEndpointRequest
{
    public Guid Id { get; set; }
}
