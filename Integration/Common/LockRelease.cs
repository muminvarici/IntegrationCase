namespace Integration.Common;

public class LockRelease : IDisposable
{
    private readonly Action _releaseAction;
    private bool _disposed;

    public LockRelease(Action releaseAction)
    {
        _releaseAction = releaseAction ?? throw new ArgumentNullException(nameof(releaseAction));
    }

    public void Dispose()
    {
        if (_disposed) return;
        
        _releaseAction.Invoke();
        _disposed = true;
    }
}