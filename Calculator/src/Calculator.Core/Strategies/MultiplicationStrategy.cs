namespace Calculator.Core.Strategies;

public class MultiplicationStrategy: IOperationStrategy
{
    public char OperatorSymbol => '*';
    public double Execute(double left, double right)
    {
        return left * right;
    }
}