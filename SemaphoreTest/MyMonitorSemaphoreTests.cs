using Semaphore.ISemaphore;
using Semaphore.Semaphore;

namespace SemaphoreTest;

[TestClass]
public class MyMonitorSemaphoreTests
{
    [TestMethod]
    public void TestAcquire()
    {
        ISemaphore semaphore = new MyMonitorSemaphore(1);
        semaphore.Acquire();
        bool result = semaphore.TryAcquire();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void TestTryAcquire_Success()
    {
        ISemaphore semaphore = new MyMonitorSemaphore(1);
        bool result = semaphore.TryAcquire();
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestTryAcquire_Failure()
    {
        ISemaphore semaphore = new MyMonitorSemaphore(1);
        semaphore.Acquire();
        bool result = semaphore.TryAcquire();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void TestRelease()
    {
        ISemaphore semaphore = new MyMonitorSemaphore(1);
        semaphore.Acquire();
        semaphore.Release(1);
        bool result = semaphore.TryAcquire();
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestReleaseMultipleTimes()
    {
        ISemaphore semaphore = new MyMonitorSemaphore(1);
        semaphore.Acquire();
        int previousCount = semaphore.Release(1); // Один Семафор, не превышаем максимум
        Assert.AreEqual(0, previousCount); // До освобождения, count = 0
        semaphore.Acquire();
        previousCount = semaphore.Release(1); // Второй Семафор
        Assert.AreEqual(0, previousCount);
        semaphore.Acquire();
        previousCount = semaphore.Release(1); // Третий Семафор
        Assert.AreEqual(0, previousCount);
        bool result = semaphore.TryAcquire();
        Assert.IsTrue(result); // После освобождения можно подхватить семафор
    }
}