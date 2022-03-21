using CFU.UniversityManagement.Application.Structure.Commands;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Structure.Departments;

public class DisbandDepartmentEndpoint : Endpoint<DisbandDepartmentRequest>
{
    private readonly IMediator _mediator;

    public DisbandDepartmentEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete(StructureRoutes.DepartmentById);
        AllowAnonymous();
    }

    public async override Task HandleAsync(DisbandDepartmentRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new DisbandDepartmentCommand(new DepartmentId(req.Id)), ct);
        await result.Match(success => SendOkAsync(cancellation: ct), notFound => SendNotFoundAsync(ct));
    }
}

public record DisbandDepartmentRequest
{
    public Guid Id { get; set; }
}
