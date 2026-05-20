using System.Diagnostics;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask2;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask2.DtoModels;

namespace Study.LabWork2.Feature.Task1.SubTask2;

public sealed class NumberSetProcessor : INumberSetProcessor
{
    private const int SetsCount = 15;
    private const int NumbersInSet = 100;
    private const int MinNumber = 1;
    private const int MaxNumber = 100;

    private readonly object _resultsLocker = new();
    private readonly object _consoleLocker = new();

    public ProcessingResultDto Process(
        IReadOnlyList<IReadOnlyList<int>> numberSets,
        int maxParallelThreads,
        bool verbose = false)
    {
        ValidateNumberSets(numberSets);

        if (maxParallelThreads <= 0)
        {
            throw new ArgumentException("Количество потоков должно быть больше нуля.");
        }

        List<ResultEntryDto> results = new();
        List<Thread> threads = new();

        using Semaphore semaphore = new(maxParallelThreads, maxParallelThreads);
        using Mutex totalMutex = new();

        int totalSum = 0;

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < numberSets.Count; i++)
        {
            int setNumber = i + 1;
            IReadOnlyList<int> numbers = numberSets[i];

            Thread thread = new(() =>
            {
                semaphore.WaitOne();

                try
                {
                    int threadId = Environment.CurrentManagedThreadId;
                    int sum = numbers.Sum();

                    lock (_resultsLocker)
                    {
                        results.Add(new ResultEntryDto(setNumber, sum, threadId));
                    }

                    totalMutex.WaitOne();

                    try
                    {
                        totalSum += sum;
                    }
                    finally
                    {
                        totalMutex.ReleaseMutex();
                    }

                    if (verbose)
                    {
                        lock (_consoleLocker)
                        {
                            Console.WriteLine($"Набор {setNumber}: сумма = {sum}, поток = {threadId}");
                        }
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            });

            threads.Add(thread);
            thread.Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        stopwatch.Stop();

        List<ResultEntryDto> orderedResults = results
            .OrderBy(result => result.SetNumber)
            .ToList();

        if (verbose)
        {
            Console.WriteLine($"Общий итог по всем наборам: {totalSum}");
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }

        return new ProcessingResultDto(
            orderedResults,
            totalSum,
            maxParallelThreads,
            stopwatch.ElapsedMilliseconds);
    }

    public ProcessingResultDto Process(
        string filePath,
        int maxParallelThreads,
        bool verbose = false)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Путь к файлу не может быть пустым.");
        }

        EnsureNumberSetsFileExists(filePath);

        IReadOnlyList<IReadOnlyList<int>> numberSets = LoadNumberSets(filePath);

        return Process(numberSets, maxParallelThreads, verbose);
    }

    public static IReadOnlyList<IReadOnlyList<int>> LoadNumberSets(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Путь к файлу не может быть пустым.");
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл с наборами чисел не найден.", filePath);
        }

        string[] lines = File.ReadAllLines(filePath)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToArray();

        List<IReadOnlyList<int>> result = new();

        foreach (string line in lines)
        {
            int[] numbers = line
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            result.Add(numbers);
        }

        ValidateNumberSets(result);

        return result;
    }

    private static void ValidateNumberSets(IReadOnlyList<IReadOnlyList<int>> numberSets)
    {
        ArgumentNullException.ThrowIfNull(numberSets);

        if (numberSets.Count != SetsCount)
        {
            throw new InvalidOperationException("Должно быть ровно 15 наборов чисел.");
        }

        for (int i = 0; i < numberSets.Count; i++)
        {
            IReadOnlyList<int> numbers = numberSets[i];

            if (numbers is null)
            {
                throw new InvalidOperationException($"Набор №{i + 1} не может быть null.");
            }

            if (numbers.Count != NumbersInSet)
            {
                throw new InvalidOperationException($"Набор №{i + 1} должен содержать ровно 100 чисел.");
            }

            if (numbers.Any(number => number < MinNumber || number > MaxNumber))
            {
                throw new InvalidOperationException($"В наборе №{i + 1} все числа должны быть от 1 до 100.");
            }
        }
    }

    private static void EnsureNumberSetsFileExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            return;
        }

        string? directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        Random random = new();
        List<string> lines = new();

        for (int i = 0; i < SetsCount; i++)
        {
            int[] numbers = new int[NumbersInSet];

            for (int j = 0; j < numbers.Length; j++)
            {
                numbers[j] = random.Next(MinNumber, MaxNumber + 1);
            }

            lines.Add(string.Join(' ', numbers));
        }

        File.WriteAllLines(filePath, lines);
    }
}
