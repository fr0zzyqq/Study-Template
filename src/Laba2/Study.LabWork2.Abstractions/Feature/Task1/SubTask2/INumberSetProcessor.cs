using Study.LabWork2.Abstractions.Feature.Task1.SubTask2.DtoModels;

namespace Study.LabWork2.Abstractions.Feature.Task1.SubTask2;

public interface INumberSetProcessor
{
    ProcessingResultDto Process(
        IReadOnlyList<IReadOnlyList<int>> numberSets,
        int maxParallelThreads,
        bool verbose = false);

    ProcessingResultDto Process(
        string filePath,
        int maxParallelThreads,
        bool verbose = false);
}
