using System.Linq.Expressions;

namespace CFU.Domain.StructureContext.AcademyAggregate;

public record UniqueAcademySpecification : Specification<Academy>
{
    private readonly Academy _academy;

    public UniqueAcademySpecification(Academy academy)
    {
        _academy = academy;
    }

    public override Expression<Func<Academy, bool>> ToExpression() =>
        academy => academy.Title == _academy.Title;
}
