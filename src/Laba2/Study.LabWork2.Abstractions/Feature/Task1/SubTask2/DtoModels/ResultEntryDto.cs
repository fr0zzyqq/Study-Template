namespace Study.LabWork2.Abstractions.Feature.Task1.SubTask2.DtoModels;

public sealed class ResultEntryDto
{
    public ResultEntryDto(int setNumber, int sum, int threadId)
    {
        SetNumber = setNumber;
        Sum = sum;
        ThreadId = threadId;
    }

    public int SetNumber { get; }

    public int Sum { get; }

    public int ThreadId { get; }
}
