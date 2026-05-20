namespace Study.LabWork2.Abstractions.Feature.Task1.SubTask2.DtoModels;

public sealed class ProcessingResultDto
{
    public ProcessingResultDto(
        IReadOnlyList<ResultEntryDto> results,
        int totalSum,
        int maxParallelThreads,
        long elapsedMilliseconds)
    {
        Results = results;
        TotalSum = totalSum;
        MaxParallelThreads = maxParallelThreads;
        ElapsedMilliseconds = elapsedMilliseconds;
    }

    public IReadOnlyList<ResultEntryDto> Results { get; }

    public int TotalSum { get; }

    public int MaxParallelThreads { get; }

    public long ElapsedMilliseconds { get; }

    public int ProcessedSetsCount => Results.Count;
}
