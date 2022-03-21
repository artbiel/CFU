namespace CFU.UniversityManagement.Application.Common.Behaviors;

public class TransactionalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    private readonly ITransactionManager _unitOfWork;

    public TransactionalBehavior(ITransactionManager unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        => _unitOfWork.ExecuteInTransactionAsync(() => next(), cancellationToken);
}
