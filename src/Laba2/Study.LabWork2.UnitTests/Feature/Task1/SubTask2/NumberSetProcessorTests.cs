using Study.LabWork2.Feature.Task1.SubTask2;

namespace Study.LabWork2.UnitTests.Feature.Task1.SubTask2;

public sealed class NumberSetProcessorTests
{
    [Test]
    public void Process_WithPreparedData_ReturnsCorrectTotalSum()
    {
        IReadOnlyList<IReadOnlyList<int>> numberSets = CreatePreparedNumberSets();

        NumberSetProcessor processor = new();

        var result = processor.Process(numberSets, maxParallelThreads: 3);

        Assert.Multiple(() =>
        {
            Assert.That(result.TotalSum, Is.EqualTo(12_000));
            Assert.That(result.ProcessedSetsCount, Is.EqualTo(15));
            Assert.That(result.MaxParallelThreads, Is.EqualTo(3));
        });
    }

    [Test]
    public void Process_WithPreparedData_ReturnsCorrectSetSums()
    {
        IReadOnlyList<IReadOnlyList<int>> numberSets = CreatePreparedNumberSets();

        NumberSetProcessor processor = new();

        var result = processor.Process(numberSets, maxParallelThreads: 3);

        Assert.Multiple(() =>
        {
            Assert.That(result.Results[0].SetNumber, Is.EqualTo(1));
            Assert.That(result.Results[0].Sum, Is.EqualTo(100));

            Assert.That(result.Results[1].SetNumber, Is.EqualTo(2));
            Assert.That(result.Results[1].Sum, Is.EqualTo(200));

            Assert.That(result.Results[2].SetNumber, Is.EqualTo(3));
            Assert.That(result.Results[2].Sum, Is.EqualTo(300));

            Assert.That(result.Results[14].SetNumber, Is.EqualTo(15));
            Assert.That(result.Results[14].Sum, Is.EqualTo(1500));
        });
    }

    [Test]
    public void Process_ResultsAreOrderedBySetNumber()
    {
        IReadOnlyList<IReadOnlyList<int>> numberSets = CreatePreparedNumberSets();

        NumberSetProcessor processor = new();

        var result = processor.Process(numberSets, maxParallelThreads: 3);

        for (int i = 0; i < result.Results.Count; i++)
        {
            Assert.That(result.Results[i].SetNumber, Is.EqualTo(i + 1));
        }
    }

    [Test]
    public void Process_TotalSumEqualsSumOfAllSetResults()
    {
        IReadOnlyList<IReadOnlyList<int>> numberSets = CreatePreparedNumberSets();

        NumberSetProcessor processor = new();

        var result = processor.Process(numberSets, maxParallelThreads: 3);

        Assert.That(result.TotalSum, Is.EqualTo(result.Results.Sum(entry => entry.Sum)));
    }

    [Test]
    public void Process_EachResultContainsThreadId()
    {
        IReadOnlyList<IReadOnlyList<int>> numberSets = CreatePreparedNumberSets();

        NumberSetProcessor processor = new();

        var result = processor.Process(numberSets, maxParallelThreads: 3);

        Assert.That(result.Results.All(entry => entry.ThreadId > 0), Is.True);
    }

    [Test]
    public void Process_ZeroThreadCount_ThrowsArgumentException()
    {
        NumberSetProcessor processor = new();

        IReadOnlyList<IReadOnlyList<int>> numberSets = CreatePreparedNumberSets();

        Assert.Throws<ArgumentException>(() => processor.Process(numberSets, maxParallelThreads: 0));
    }

    [Test]
    public void Process_NegativeThreadCount_ThrowsArgumentException()
    {
        NumberSetProcessor processor = new();

        IReadOnlyList<IReadOnlyList<int>> numberSets = CreatePreparedNumberSets();

        Assert.Throws<ArgumentException>(() => processor.Process(numberSets, maxParallelThreads: -1));
    }

    [Test]
    public void Process_EmptySets_ThrowsInvalidOperationException()
    {
        NumberSetProcessor processor = new();

        IReadOnlyList<IReadOnlyList<int>> numberSets = new List<IReadOnlyList<int>>();

        Assert.Throws<InvalidOperationException>(() => processor.Process(numberSets, maxParallelThreads: 3));
    }

    [Test]
    public void Process_WrongSetsCount_ThrowsInvalidOperationException()
    {
        NumberSetProcessor processor = new();

        IReadOnlyList<IReadOnlyList<int>> numberSets = Enumerable
            .Range(1, 14)
            .Select(_ => (IReadOnlyList<int>)Enumerable.Repeat(1, 100).ToArray())
            .ToList();

        Assert.Throws<InvalidOperationException>(() => processor.Process(numberSets, maxParallelThreads: 3));
    }

    [Test]
    public void Process_WrongNumbersCountInSet_ThrowsInvalidOperationException()
    {
        NumberSetProcessor processor = new();

        List<IReadOnlyList<int>> numberSets = CreatePreparedNumberSets()
            .Select(set => (IReadOnlyList<int>)set.ToArray())
            .ToList();

        numberSets[0] = Enumerable.Repeat(1, 99).ToArray();

        Assert.Throws<InvalidOperationException>(() => processor.Process(numberSets, maxParallelThreads: 3));
    }

    [Test]
    public void Process_NumberLessThanOne_ThrowsInvalidOperationException()
    {
        NumberSetProcessor processor = new();

        List<IReadOnlyList<int>> numberSets = CreatePreparedNumberSets()
            .Select(set => (IReadOnlyList<int>)set.ToArray())
            .ToList();

        numberSets[0] = Enumerable.Repeat(0, 100).ToArray();

        Assert.Throws<InvalidOperationException>(() => processor.Process(numberSets, maxParallelThreads: 3));
    }

    [Test]
    public void Process_NumberGreaterThanOneHundred_ThrowsInvalidOperationException()
    {
        NumberSetProcessor processor = new();

        List<IReadOnlyList<int>> numberSets = CreatePreparedNumberSets()
            .Select(set => (IReadOnlyList<int>)set.ToArray())
            .ToList();

        numberSets[0] = Enumerable.Repeat(101, 100).ToArray();

        Assert.Throws<InvalidOperationException>(() => processor.Process(numberSets, maxParallelThreads: 3));
    }

    [Test]
    public void LoadNumberSets_ValidFile_ReturnsFifteenSetsWithOneHundredNumbers()
    {
        string filePath = CreateTempFilePath();

        try
        {
            File.WriteAllLines(filePath, CreateFileLines());

            IReadOnlyList<IReadOnlyList<int>> result = NumberSetProcessor.LoadNumberSets(filePath);

            Assert.Multiple(() =>
            {
                Assert.That(result.Count, Is.EqualTo(15));
                Assert.That(result.All(set => set.Count == 100), Is.True);
                Assert.That(result.All(set => set.All(number => number >= 1 && number <= 100)), Is.True);
            });
        }
        finally
        {
            DeleteFileIfExists(filePath);
        }
    }

    [Test]
    public void LoadNumberSets_FileWithWrongSetsCount_ThrowsInvalidOperationException()
    {
        string filePath = CreateTempFilePath();

        try
        {
            File.WriteAllText(filePath, string.Join(' ', Enumerable.Repeat(1, 100)));

            Assert.Throws<InvalidOperationException>(() => NumberSetProcessor.LoadNumberSets(filePath));
        }
        finally
        {
            DeleteFileIfExists(filePath);
        }
    }

    [Test]
    public void LoadNumberSets_FileWithWrongNumbersCount_ThrowsInvalidOperationException()
    {
        string filePath = CreateTempFilePath();

        try
        {
            List<string> lines = CreateFileLines();
            lines[0] = "1 2 3";

            File.WriteAllLines(filePath, lines);

            Assert.Throws<InvalidOperationException>(() => NumberSetProcessor.LoadNumberSets(filePath));
        }
        finally
        {
            DeleteFileIfExists(filePath);
        }
    }

    [Test]
    public void LoadNumberSets_FileWithNumberOutOfRange_ThrowsInvalidOperationException()
    {
        string filePath = CreateTempFilePath();

        try
        {
            List<string> lines = CreateFileLines();
            lines[0] = string.Join(' ', Enumerable.Repeat(101, 100));

            File.WriteAllLines(filePath, lines);

            Assert.Throws<InvalidOperationException>(() => NumberSetProcessor.LoadNumberSets(filePath));
        }
        finally
        {
            DeleteFileIfExists(filePath);
        }
    }

    [Test]
    public void Process_FilePath_CreatesFileAndProcessesData()
    {
        string filePath = CreateTempFilePath();

        try
        {
            NumberSetProcessor processor = new();

            var result = processor.Process(filePath, maxParallelThreads: 3);

            Assert.Multiple(() =>
            {
                Assert.That(File.Exists(filePath), Is.True);
                Assert.That(result.ProcessedSetsCount, Is.EqualTo(15));
                Assert.That(result.Results.Count, Is.EqualTo(15));
                Assert.That(result.TotalSum, Is.EqualTo(result.Results.Sum(entry => entry.Sum)));
            });
        }
        finally
        {
            DeleteFileIfExists(filePath);
        }
    }

    private static IReadOnlyList<IReadOnlyList<int>> CreatePreparedNumberSets()
    {
        List<IReadOnlyList<int>> sets = new();

        for (int i = 1; i <= 15; i++)
        {
            sets.Add(Enumerable.Repeat(i, 100).ToArray());
        }

        return sets;
    }

    private static List<string> CreateFileLines()
    {
        List<string> lines = new();

        for (int i = 1; i <= 15; i++)
        {
            lines.Add(string.Join(' ', Enumerable.Repeat(i, 100)));
        }

        return lines;
    }

    private static string CreateTempFilePath()
    {
        return Path.Combine(Path.GetTempPath(), $"number_sets_{Guid.NewGuid()}.txt");
    }

    private static void DeleteFileIfExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
