namespace Semaphore.Semaphore;

public class MyMonitorSemaphore : ISemaphore.ISemaphore
{
    private readonly object _lock = new object();
    private int _count;
    private readonly int _initialCount;

    public MyMonitorSemaphore(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));
        
        _count = count;
        _initialCount = count;
    }

    public void Acquire()
    {
        lock (_lock)
        {
            while (_count == 0)
            {
                Monitor.Wait(_lock);
            }

            _count--;
        }
    }

    public bool TryAcquire()
    {
        lock (_lock)
        {
            if (_count > 0)
            {
                _count--;
                return true;
            }

            return false;
        }
    }

    public int Release(int releaseCount)
    {
        if (releaseCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(releaseCount));

        lock (_lock)
        {
            int previousCount = _count;
            _count += releaseCount;

            if (_count > _initialCount)
            {
                _count = _initialCount;
                throw new SemaphoreFullException("Превышение максимального счетчика.");
            }

            Monitor.PulseAll(_lock);
            return previousCount;
        }
    }
}