namespace Calculator.Core;

public interface ICalculatorSession
{
    void ExecuteExpression(string expression);
    void UndoLast();
    double CurrentValue { get; }
}