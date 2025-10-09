namespace Calculator;

public class Calculator: ICalculator
{
    private readonly Dictionary<char, IOperation> _operations;

    public Calculator(IEnumerable<IOperation> operations)
    {
        _operations = operations.ToDictionary(o => o.Operator);
    }
    
    public double Calculate(double leftOperand, char operation, double rightOperand)
    {
        if(_operations.TryGetValue(operation, out IOperation op)) 
            return op.Execute(leftOperand, rightOperand);

        throw new ArgumentException($"Unknown operator: {operation}");
    }
}