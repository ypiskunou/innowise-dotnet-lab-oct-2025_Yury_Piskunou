using System.Diagnostics;

namespace AsyncDemo;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("--- Демонстрация синхронной и асинхронной работы ---");
        var stopwatch = new Stopwatch();
        
        Console.WriteLine("\nЗапуск синхронной обработки...");
        stopwatch.Start();
        RunSynchronous();
        stopwatch.Stop();
        Console.WriteLine($"Синхронная обработка завершена за: {stopwatch.Elapsed.TotalSeconds:F2} секунд.");
        
        Console.WriteLine("\nЗапуск асинхронной обработки...");
        stopwatch.Restart();
        await RunAsynchronous();
        stopwatch.Stop();
        Console.WriteLine($"Асинхронная обработка завершена за: {stopwatch.Elapsed.TotalSeconds:F2} секунд.");
    }
    
    public static void RunSynchronous()
    {
        Console.WriteLine(ProcessData("Файл 1"));
        Console.WriteLine(ProcessData("Файл 2"));
        Console.WriteLine(ProcessData("Файл 3"));
    }

    public static string ProcessData(string dataName)
    {
        Console.WriteLine($" > Начинается обработка '{dataName}'...");
        Thread.Sleep(3000);
        return $"   Обработка '{dataName}' завершена за 3 секунды.";
    }
    
    public static async Task RunAsynchronous()
    {
        Task<string> task1 = ProcessDataAsync("Файл 1");
        Task<string> task2 = ProcessDataAsync("Файл 2");
        Task<string> task3 = ProcessDataAsync("Файл 3");
        
        await Task.WhenAll(task1, task2, task3);
        
        Console.WriteLine(await task1);
        Console.WriteLine(await task2);
        Console.WriteLine(await task3);
    }

    public static async Task<string> ProcessDataAsync(string dataName)
    {
        Console.WriteLine($" > Начинается обработка '{dataName}'...");
        await Task.Delay(3000);
        return $"   Обработка '{dataName}' завершена за 3 секунды.";
    }
}