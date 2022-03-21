namespace CFU.UniversityManagement.Application.Abstractions;

public interface ICacheableQuery<T> : IQuery<T>
{
    string CacheKey { get; }
    TimeSpan AbsoluteExpirationRelativeToNow { get; }
}
