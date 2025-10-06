namespace Calculator.Core;

public interface ICommand
{
    double Execute();
    double UnExecute();
}