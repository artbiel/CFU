namespace CFU.UniversityManagement.Application.Abstractions;

public interface ITransactionManager
{
    Task<TResponse> ExecuteInTransactionAsync<TResponse>(Func<Task<TResponse>> operation, CancellationToken cancellationToken);
}

