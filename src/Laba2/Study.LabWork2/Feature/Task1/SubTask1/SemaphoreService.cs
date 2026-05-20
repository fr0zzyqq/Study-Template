using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask1.DtoModels;

namespace Study.LabWork2.Feature.Task1.SubTask1;

public sealed class SemaphoreService : IPrimeCounter
{
    public PrimeCountResultDto CountPrimes(int start, int end, int threadCount)
    {
        int totalCount = 0;
        var primes = new List<int>();
        var semaphore = new Semaphore(1, 1);
        object consoleLocker = new();

        var watch = System.Diagnostics.Stopwatch.StartNew();

        Thread[] threads = new Thread[threadCount];
        int range = (end - start + 1) / threadCount;

        for (int t = 0; t < threadCount; t++)
        {
            int s = start + t * range;
            int e = (t == threadCount - 1) ? end : s + range - 1;
            int num = t + 1;

            threads[t] = new Thread(() =>
            {
                for (int i = s; i <= e; i++)
                {
                    lock (consoleLocker)
                        Console.WriteLine($"Поток {num}: проверяю число {i}");

                    if (IsPrime(i))
                    {
                        semaphore.WaitOne();
                        try
                        {
                            totalCount++;
                            primes.Add(i);
                        }
                        finally
                        {
                            semaphore.Release();
                        }

                        lock (consoleLocker)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Поток {num}: НАЙДЕНО {i}, всего {totalCount}");
                            Console.ResetColor();
                        }
                    }
                }
            });

            threads[t].Start();
        }

        foreach (var thread in threads)
            thread.Join();

        watch.Stop();

        return new PrimeCountResultDto
        {
            PrimeCount = totalCount,
            ExecutionTime = watch.Elapsed,
            ThreadCount = threadCount,
            SynchronizationType = "Semaphore",
            FoundPrimes = primes
        };
    }

    public string GetVersionName() => "Semaphore";

    private static bool IsPrime(int n)
    {
        if (n <= 1) return false;
        if (n == 2) return true;
        if (n % 2 == 0) return false;
        for (int i = 3; i * i <= n; i += 2)
            if (n % i == 0) return false;
        return true;
    }
}
