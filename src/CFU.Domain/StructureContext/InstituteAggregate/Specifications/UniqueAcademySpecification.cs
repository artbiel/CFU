using System.Linq.Expressions;

namespace CFU.Domain.StructureContext.InstituteAggregate;

public record UniqueInstituteSpecification : Specification<Institute>
{
    private readonly Institute _institute;

    public UniqueInstituteSpecification(Institute Institute)
    {
        _institute = Institute;
    }

    public override Expression<Func<Institute, bool>> ToExpression() =>
        Institute => Institute.Title == _institute.Title;
}
