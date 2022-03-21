using CFU.UniversityManagement.Application.Structure.Commands;
using CFU.UniversityManagement.Application.Structure.DTOs;

namespace CFU.UniversityManagement.WebAPI.Endpoints.Structure.Faculties;

public class CreateFacultyDepartmentEndpoint : Endpoint<CreateFacultyDepartmentEndpointRequest, CreateFacultyDepartmentEndpointResponse>
{
    private readonly IMediator _mediator;

    public CreateFacultyDepartmentEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(StructureRoutes.FacultyDepartments);
        AllowAnonymous();
    }

    public async override Task HandleAsync(CreateFacultyDepartmentEndpointRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateFacultyDepartmentCommand(new FacultyId(req.FacultyId), req.Title), ct);
        await result.Match(success => SendAsync(new(success.Value), cancellation: ct), notFound => SendNotFoundAsync(ct));
    }
}

public record CreateFacultyDepartmentEndpointRequest
{
    public Guid FacultyId { get; set; }
    public string? Title { get; set; }
}

public record struct CreateFacultyDepartmentEndpointResponse(DepartmentDTO Department);
