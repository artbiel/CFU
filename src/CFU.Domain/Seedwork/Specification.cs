using System.Linq.Expressions;

namespace CFU.Domain.Seedwork;

public abstract record Specification<T> : ISpecification<T>
{
    public bool IsSatisfiedBy(T entity) =>
        ToExpression().Compile()(entity);

    public abstract Expression<Func<T, bool>> ToExpression();

    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification) =>
        specification.ToExpression();
}

