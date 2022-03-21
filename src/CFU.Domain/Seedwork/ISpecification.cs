using System.Linq.Expressions;

namespace CFU.Domain.Seedwork;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
    Expression<Func<T, bool>> ToExpression();
}

