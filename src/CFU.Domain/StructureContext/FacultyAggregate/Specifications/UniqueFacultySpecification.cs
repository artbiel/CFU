using System.Linq.Expressions;

namespace CFU.Domain.StructureContext.FacultyAggregate;

public record UniqueFacultySpecification : Specification<Faculty>
{
    private readonly Faculty _faculty;

    public UniqueFacultySpecification(Faculty faculty)
    {
        _faculty = faculty;
    }

    public override Expression<Func<Faculty, bool>> ToExpression() =>
        faculty => faculty.Title == _faculty.Title;
}
