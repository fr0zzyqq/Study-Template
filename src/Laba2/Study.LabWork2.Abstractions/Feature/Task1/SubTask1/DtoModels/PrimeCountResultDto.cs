namespace Study.LabWork2.Abstractions.Feature.Task1.SubTask1.DtoModels;

public sealed class PrimeCountResultDto
{
    public PrimeCountResultDto(
        int primeCount,
        int threadCount,
        string synchronizationType,
        IReadOnlyList<int> foundPrimes,
        long elapsedMilliseconds)
    {
        PrimeCount = primeCount;
        ThreadCount = threadCount;
        SynchronizationType = synchronizationType;
        FoundPrimes = foundPrimes;
        ElapsedMilliseconds = elapsedMilliseconds;
    }

    public int PrimeCount { get; }

    public int ThreadCount { get; }

    public string SynchronizationType { get; }

    public IReadOnlyList<int> FoundPrimes { get; }

    public long ElapsedMilliseconds { get; }
}
