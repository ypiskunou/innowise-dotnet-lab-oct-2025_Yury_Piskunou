namespace Calculator.Core.Strategies;

public class AdditionStrategy : IOperationStrategy
{
    public char OperatorSymbol => '+';
    public double Execute(double left, double right) => left + right;
}
