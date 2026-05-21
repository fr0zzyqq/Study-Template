using System;
using Study.LabWork1.Features.Task1;

namespace Study.LabWork1.UnitTests.Features.Task1
{
    public static class ComplexNumberTests
    {
        public static void Run()
        {
            Console.WriteLine("=== Тесты ComplexNumber ===\n");

            var a = new ComplexNumber(2, 3);
            var b = new ComplexNumber(4, 5);
            
            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");
            Console.WriteLine($"a + b = {a + b}");
            Console.WriteLine($"a - b = {a - b}");
            Console.WriteLine($"a * b = {a * b}");
            Console.WriteLine($"a / b = {a / b}");
            Console.WriteLine($"|a| = {+a}");
            Console.WriteLine($"Сопряжённое a = {-a}");
            Console.WriteLine($"a == a: {a == a}");
            Console.WriteLine($"a == b: {a == b}");
            Console.WriteLine($"Чисто действительное: {new ComplexNumber(5, 0)}");
            Console.WriteLine($"Чисто мнимое: {new ComplexNumber(0, 7)}");

            Console.WriteLine("\n=== Тесты пройдены ===");
        }
    }
}