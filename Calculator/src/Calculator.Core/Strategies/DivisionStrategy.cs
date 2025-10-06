namespace Calculator.Core.Strategies;

public class DivisionStrategy : IOperationStrategy
{
    public char OperatorSymbol => '/';
    public double Execute(double left, double right)
    {
        if (right == 0) throw new DivideByZeroException("Деление на ноль.");
        return left / right;
    }
}