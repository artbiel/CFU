using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CFU.UniversityManagement.Application.Common.Behaviors;

public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheableQuery<TResponse>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CachingBehaviour<TRequest, TResponse>> _logger;
    public CachingBehaviour(IDistributedCache cache, ILogger<CachingBehaviour<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = request.GetType();
        _logger.LogInformation("{Request} is configured for caching", requestName);

        var cachedValue = await _cache.GetStringAsync(request.CacheKey, cancellationToken);
        if (cachedValue is not null) {
            _logger.LogInformation("Returning cached value for {Request}", requestName);
            return JsonSerializer.Deserialize<TResponse>(cachedValue);
        }

        _logger.LogInformation("{Request} Cache Key: {Key} is not inside the cache, executing request", requestName, request.CacheKey);

        var response = await next();

        await _cache.SetStringAsync(request.CacheKey,
            JsonSerializer.Serialize(response),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = request.AbsoluteExpirationRelativeToNow },
            cancellationToken);

        return response;
    }
}
