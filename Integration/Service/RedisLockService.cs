using Integration.Common;
using Integration.Service.Abstractions;
using StackExchange.Redis;

namespace Integration.Service;

public class RedisLockService : IDistributedLockService
{
    //We need to move the connection string to appsettings.json etc. in practice but it's ok for development
    private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new(() => ConnectionMultiplexer.Connect("localhost:6379"));

    private static readonly object LockObject = new();
    private static RedisLockService _instance;

    private RedisLockService()
    {
    }

    /// <summary>
    /// Singleton implementation for the related class
    /// </summary>
    public static IDistributedLockService GetInstance()
    {
        lock (LockObject)
        {
            return _instance ??= new RedisLockService();
        }
    }

    /// <summary>
    /// Method to acquire a distributed lock
    /// </summary>
    public IDisposable AcquireLock(string lockKey, TimeSpan lockTimeout)
    {
        var database = LazyConnection.Value.GetDatabase();
        const string lockValue = "1";

        var lockTaken = database.LockTake(lockKey, lockValue, lockTimeout);

        return !lockTaken
            ? null
            : new LockRelease(() => database.LockRelease(lockKey, lockValue));
    }
}