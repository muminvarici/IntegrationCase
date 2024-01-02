namespace Integration.Service.Abstractions;

// We can implement distributed lock mechanism over redis, mongodb or any other database provider.
// To achieve the release or lock timeout, we can use TTL(Time to live) for mongodb.
// If the provider doesn't support like mongo, we need to implement a custom solution. 
public interface IDistributedLockService
{
    /// <summary>
    /// Method to acquire a distributed lock
    /// </summary>
    IDisposable AcquireLock(string lockKey, TimeSpan lockTimeout);
}