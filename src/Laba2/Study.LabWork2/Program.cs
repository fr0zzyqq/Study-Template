using Study.LabWork2.Feature.Task1.SubTask1;
using Study.LabWork2.Feature.Task1.SubTask2;

namespace Study.LabWork2;

public static class Program
{
    public static void Main()
    {
        // ==========================================
        // ЗАДАНИЕ 1.1 — ПРОСТЫЕ ЧИСЛА
        // ==========================================

        Console.WriteLine("=== ВЕРСИЯ 1: MONITOR ===\n");
        var result1 = new MonitorService().CountPrimes(1, 10000, 4);
        Console.WriteLine(result1);
        Console.WriteLine(result1.IsValid(1229) ? "✓ ВЕРНО\n" : "✗ ОШИБКА\n");

        Console.WriteLine("=== ВЕРСИЯ 2: MUTEX ===\n");
        var result2 = new MutexService().CountPrimes(1, 10000, 4);
        Console.WriteLine(result2);
        Console.WriteLine(result2.IsValid(1229) ? "✓ ВЕРНО\n" : "✗ ОШИБКА\n");

        Console.WriteLine("=== ВЕРСИЯ 3: SEMAPHORE ===\n");
        var result3 = new SemaphoreService().CountPrimes(1, 10000, 4);
        Console.WriteLine(result3);
        Console.WriteLine(result3.IsValid(1229) ? "✓ ВЕРНО\n" : "✗ ОШИБКА\n");

        // ==========================================
        // ЗАДАНИЕ 1.2 — НАБОРЫ ЧИСЕЛ
        // ==========================================

        Console.WriteLine("=== ОБРАБОТКА НАБОРОВ ЧИСЕЛ ===\n");

        var processor = new NumberSetProcessor(maxThreads: 3);
        processor.Process();
        var procResult = processor.GetResult();

        Console.WriteLine($"Обработано наборов: {procResult.ProcessedSetsCount}");
        Console.WriteLine($"Время: {procResult.ExecutionTime.TotalMilliseconds:F2} мс\n");

        foreach (var entry in procResult.Results)
        {
            Console.WriteLine(entry.ToString());
        }

        Console.WriteLine(new string('-', 40));
        Console.WriteLine($"ОБЩИЙ ИТОГ: {procResult.TotalSum}");
    }
}
