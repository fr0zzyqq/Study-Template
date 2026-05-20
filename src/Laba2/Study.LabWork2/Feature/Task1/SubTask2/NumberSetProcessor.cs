using Study.LabWork2.Abstractions.Feature.Task1.SubTask2;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask2.DtoModels;

namespace Study.LabWork2.Feature.Task1.SubTask2;

public sealed class NumberSetProcessor : INumberSetProcessor
{
    private ProcessingResultDto? _result;

    // Для хранения наборов чисел
    private readonly List<int[]> _numberSets;

    // Semaphore — максимум n потоков одновременно
    private readonly Semaphore _semaphore;

    // Monitor (lock) — для добавления в общий список результатов
    private readonly object _resultsLocker = new();

    // Mutex — для общего итога
    private readonly Mutex _totalSumMutex = new();

    // Общий список результатов
    private readonly List<ResultEntryDto> _results = new();

    // Общий итог
    private int _totalSum;

    public NumberSetProcessor(int maxThreads = 4)
    {
        _semaphore = new Semaphore(maxThreads, maxThreads);

        // Загружаем наборы из файла
        _numberSets = LoadNumberSets();
    }

    private List<int[]> LoadNumberSets()
    {
        var sets = new List<int[]>();

        // Ищем файл в папке с проектом
        string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "..", "..", "..",
            "Feature", "Task1", "SubTask2", "NumberSets.txt");

        if (!File.Exists(filePath))
        {
            // Если файла нет — генерируем наборы на лету (с фиксированным seed)
            var rnd = new Random(42);
            for (int i = 0; i < 15; i++)
            {
                sets.Add(Enumerable.Range(0, 100).Select(_ => rnd.Next(1, 101)).ToArray());
            }
            return sets;
        }

        foreach (string line in File.ReadAllLines(filePath))
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                sets.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray());
            }
        }

        return sets;
    }

    public void Process()
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();

        Thread[] threads = new Thread[_numberSets.Count];

        for (int i = 0; i < _numberSets.Count; i++)
        {
            int setIndex = i;
            int[] numbers = _numberSets[i];

            threads[i] = new Thread(() =>
            {
                // Ждём разрешения от Semaphore
                _semaphore.WaitOne();
                try
                {
                    // Считаем сумму набора
                    int sum = numbers.Sum();

                    // Добавляем результат в общий список (Monitor/lock)
                    lock (_resultsLocker)
                    {
                        _results.Add(new ResultEntryDto
                        {
                            SetNumber = setIndex + 1,
                            Sum = sum,
                            ThreadId = Environment.CurrentManagedThreadId
                        });
                    }

                    // Обновляем общий итог (Mutex)
                    _totalSumMutex.WaitOne();
                    try
                    {
                        _totalSum += sum;
                    }
                    finally
                    {
                        _totalSumMutex.ReleaseMutex();
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            });

            threads[i].Start();
        }

        // Ждём завершения всех потоков
        foreach (var thread in threads)
        {
            thread.Join();
        }

        watch.Stop();

        _result = new ProcessingResultDto
        {
            Results = _results.OrderBy(r => r.SetNumber).ToList(),
            TotalSum = _totalSum,
            ExecutionTime = watch.Elapsed,
            ProcessedSetsCount = _results.Count
        };
    }

    public ProcessingResultDto GetResult()
    {
        return _result ?? throw new InvalidOperationException(
            "Сначала вызовите метод Process()");
    }
}
