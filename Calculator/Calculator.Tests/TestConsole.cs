namespace Calculator.Tests;

using System.Collections.Generic;
using System.Linq;

public class TestConsole : IConsole
{
    private readonly Queue<string?> _inputs = new Queue<string?>();
    
    public readonly List<string> AllOutput = new List<string>();
    
    public void FeedInput(params string?[] inputs)
    {
        foreach (var input in inputs)
        {
            _inputs.Enqueue(input);
        }
    }
    
    public string? ReadLine()
    {
        return _inputs.Count > 0 ? _inputs.Dequeue() : null;
    }
    
    public void WriteLine(string message) => AllOutput.Add(message);
    public void Write(string message) => AllOutput.Add(message);
    
    public string FullOutput => string.Join("", AllOutput);
}