using Study.LabWork2.Feature.Task1.SubTask1;

namespace Study.LabWork2.UnitTests.Feature.Task1.SubTask1;

public sealed class SemaphoreServiceTests
{
    [Test]
    public void CountPrimes_FromOneToTenThousand_Returns1229()
    {
        using SemaphoreService service = new();

        var result = service.CountPrimes(1, 10_000, 4);

        Assert.Multiple(() =>
        {
            Assert.That(result.PrimeCount, Is.EqualTo(1229));
            Assert.That(result.ThreadCount, Is.EqualTo(4));
            Assert.That(result.SynchronizationType, Is.EqualTo("Semaphore"));
            Assert.That(result.FoundPrimes.Count, Is.EqualTo(1229));
        });
    }

    [Test]
    public void CountPrimes_FromOneToTen_ReturnsFour()
    {
        using SemaphoreService service = new();

        var result = service.CountPrimes(1, 10, 2);

        Assert.Multiple(() =>
        {
            Assert.That(result.PrimeCount, Is.EqualTo(4));
            Assert.That(result.FoundPrimes, Is.EqualTo(new[] { 2, 3, 5, 7 }));
        });
    }

    [Test]
    public void CountPrimes_FromEightToTen_ReturnsZero()
    {
        using SemaphoreService service = new();

        var result = service.CountPrimes(8, 10, 2);

        Assert.Multiple(() =>
        {
            Assert.That(result.PrimeCount, Is.EqualTo(0));
            Assert.That(result.FoundPrimes, Is.Empty);
        });
    }

    [Test]
    public void CountPrimes_StartGreaterThanEnd_ThrowsArgumentException()
    {
        using SemaphoreService service = new();

        Assert.Throws<ArgumentException>(() => service.CountPrimes(10, 1, 2));
    }

    [Test]
    public void CountPrimes_ZeroThreadCount_ThrowsArgumentException()
    {
        using SemaphoreService service = new();

        Assert.Throws<ArgumentException>(() => service.CountPrimes(1, 10, 0));
    }

    [Test]
    public void CountPrimes_NegativeThreadCount_ThrowsArgumentException()
    {
        using SemaphoreService service = new();

        Assert.Throws<ArgumentException>(() => service.CountPrimes(1, 10, -1));
    }
}
