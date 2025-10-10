namespace Calculator;

public interface IConsole
{
    string? ReadLine();
    void WriteLine(string message);
    void Write(string message);
    void WriteLine() => WriteLine(String.Empty);
}