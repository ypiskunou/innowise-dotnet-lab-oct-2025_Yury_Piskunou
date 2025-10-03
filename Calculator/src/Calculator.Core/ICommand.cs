namespace Calculator.Core;

public interface ICommand
{
    void Execute();
    void UnExecute();
}