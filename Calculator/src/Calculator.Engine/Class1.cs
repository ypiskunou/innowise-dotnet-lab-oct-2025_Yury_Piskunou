namespace Calculator.Engine;

using Calculator.Core;

public class CalculationService : ICalculationService
{
    public double Evaluate(string expression)
    {
        return double.Parse(expression);
    }
}