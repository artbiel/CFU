using CFU.UniversityManagement.Application.Structure.Commands;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Structure.Institutes;

public class CreateInstituteDepartmentEndpoint : Endpoint<CreateInstituteDepartmentEndpointRequest, CreateInstituteDepartmentEndpointResponse>
{
    private readonly IMediator _mediator;

    public CreateInstituteDepartmentEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(StructureRoutes.InstituteDepartments);
        AllowAnonymous();
    }

    public async override Task HandleAsync(CreateInstituteDepartmentEndpointRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateInstituteDepartmentCommand(new InstituteId(req.InstituteId), req.Title), ct);
        await result.Match(success => SendAsync(new(success.Value), cancellation: ct), notFound => SendNotFoundAsync(ct));
    }
}

public record CreateInstituteDepartmentEndpointRequest
{
    public Guid InstituteId { get; set; }
    public string? Title { get; set; }
}

public record struct CreateInstituteDepartmentEndpointResponse(DepartmentDTO Department);
