namespace Calculator;

public class SystemConsole : IConsole
{
    public string? ReadLine() => Console.ReadLine();
    public void WriteLine(string message) => Console.WriteLine(message);
    public void Write(string message) => Console.Write(message);
}