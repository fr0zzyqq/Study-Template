using System.Diagnostics;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask1.DtoModels;

namespace Study.LabWork2.Feature.Task1.SubTask1;

public sealed class SemaphoreService : IPrimeCounter, IDisposable
{
    private readonly Semaphore _semaphore = new(1, 1);
    private readonly object _consoleLocker = new();

    public PrimeCountResultDto CountPrimes(
        int startNumber,
        int endNumber,
        int threadCount,
        bool verbose = false)
    {
        ValidateInput(startNumber, endNumber, threadCount);

        int primeCount = 0;
        List<int> foundPrimes = new();
        List<Thread> threads = new();
        List<(int Start, int End)> ranges = SplitRange(startNumber, endNumber, threadCount);

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < threadCount; i++)
        {
            int threadNumber = i + 1;
            int localStart = ranges[i].Start;
            int localEnd = ranges[i].End;

            Thread thread = new(() =>
            {
                for (int number = localStart; number <= localEnd; number++)
                {
                    bool isPrime = PrimeNumberHelper.IsPrime(number);

                    if (verbose)
                    {
                        PrintThreadInfo(threadNumber, number, isPrime);
                    }

                    if (!isPrime)
                    {
                        continue;
                    }

                    _semaphore.WaitOne();

                    try
                    {
                        primeCount++;
                        foundPrimes.Add(number);
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
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

        List<int> orderedPrimes = foundPrimes
            .OrderBy(number => number)
            .ToList();

        if (verbose)
        {
            Console.WriteLine($"Общее количество простых чисел: {primeCount}");
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }

        return new PrimeCountResultDto(
            primeCount,
            threadCount,
            "Semaphore",
            orderedPrimes,
            stopwatch.ElapsedMilliseconds);
    }

    public void Dispose()
    {
        _semaphore.Dispose();
    }

    private void PrintThreadInfo(int threadNumber, int number, bool isPrime)
    {
        lock (_consoleLocker)
        {
            Console.WriteLine($"Поток {threadNumber}: проверяется число {number}");

            if (isPrime)
            {
                Console.WriteLine($"Поток {threadNumber}: найдено простое число {number}");
            }
        }
    }

    private static void ValidateInput(int startNumber, int endNumber, int threadCount)
    {
        if (startNumber > endNumber)
        {
            throw new ArgumentException("Начало диапазона не может быть больше конца диапазона.");
        }

        if (threadCount <= 0)
        {
            throw new ArgumentException("Количество потоков должно быть больше нуля.");
        }
    }

    private static List<(int Start, int End)> SplitRange(int startNumber, int endNumber, int parts)
    {
        List<(int Start, int End)> ranges = new();

        int totalNumbers = endNumber - startNumber + 1;
        int baseSize = totalNumbers / parts;
        int remainder = totalNumbers % parts;

        int currentStart = startNumber;

        for (int i = 0; i < parts; i++)
        {
            int currentSize = baseSize + (i < remainder ? 1 : 0);
            int currentEnd = currentStart + currentSize - 1;

            ranges.Add((currentStart, currentEnd));

            currentStart = currentEnd + 1;
        }

        return ranges;
    }
}
