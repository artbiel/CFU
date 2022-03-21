using CFU.UniversityManagement.Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace CFU.UniversityManagement.Infrastructure;

public class TransactionManager : ITransactionManager
{
    private readonly UniversityManagementDBContext _context;
    private readonly ILogger<TransactionManager> _logger;

    public TransactionManager(UniversityManagementDBContext context, ILogger<TransactionManager> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TResponse> ExecuteInTransactionAsync<TResponse>(Func<Task<TResponse>> operation, CancellationToken cancellationToken)
    {
        var response = default(TResponse)!;

        try {
            if (_context.HasActiveTransaction) {
                return await operation();
            }

            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async (token) => {
                using var transaction = await _context.BeginTransactionAsync(token);

                response = await operation();

                await _context.CommitTransactionAsync(transaction, token);
            }, cancellationToken);

            return response;
        }
        catch (Exception ex) {
            _logger.LogError(ex, "ERROR Handling transaction");

            throw;
        }
    }
}
