using System.Text;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask2.DtoModels;
using Study.LabWork2.Feature.Task1.SubTask1;
using Study.LabWork2.Feature.Task1.SubTask2;

namespace Study.LabWork2;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Лабораторная работа 2");
        Console.WriteLine();

        Console.WriteLine("Задание 1.1");
        Console.WriteLine();
        RunSubTask1();

        Console.WriteLine();
        Console.WriteLine("Задание 1.2");
        Console.WriteLine();
        RunSubTask2();
    }

    private static void RunSubTask1()
    {
        MonitorService monitorService = new();

        using MutexService mutexService = new();
        using SemaphoreService semaphoreService = new();

        var monitorResult = monitorService.CountPrimes(1, 10_000, 4, verbose: true);
        Console.WriteLine($"Monitor: {monitorResult.PrimeCount}, время: {monitorResult.ElapsedMilliseconds} мс");
        Console.WriteLine();

        var mutexResult = mutexService.CountPrimes(1, 10_000, 4, verbose: true);
        Console.WriteLine($"Mutex: {mutexResult.PrimeCount}, время: {mutexResult.ElapsedMilliseconds} мс");
        Console.WriteLine();

        var semaphoreResult = semaphoreService.CountPrimes(1, 10_000, 4, verbose: true);
        Console.WriteLine($"Semaphore: {semaphoreResult.PrimeCount}, время: {semaphoreResult.ElapsedMilliseconds} мс");
    }

    private static void RunSubTask2()
    {
        NumberSetProcessor processor = new();

        string filePath = Path.Combine("Data", "number-sets.txt");

        var result = processor.Process(filePath, maxParallelThreads: 3, verbose: true);

        Console.WriteLine();

        foreach (ResultEntryDto entry in result.Results)
        {
            Console.WriteLine($"Набор {entry.SetNumber}: сумма = {entry.Sum}, поток = {entry.ThreadId}");
        }

        Console.WriteLine($"Общий итог: {result.TotalSum}");
        Console.WriteLine($"Время выполнения: {result.ElapsedMilliseconds} мс");
    }
}
