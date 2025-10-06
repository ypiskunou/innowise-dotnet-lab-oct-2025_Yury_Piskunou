namespace Calculator.Core;

public interface IOperationStrategy
{
    char OperatorSymbol { get; }
    double Execute(double left, double right);
}