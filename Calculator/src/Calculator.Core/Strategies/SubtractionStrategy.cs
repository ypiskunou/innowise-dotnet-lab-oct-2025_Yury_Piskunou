namespace Calculator.Core.Strategies;

public class SubtractionStrategy : IOperationStrategy
{
    public char OperatorSymbol => '-';
    public double Execute(double left, double right) => left - right;
}