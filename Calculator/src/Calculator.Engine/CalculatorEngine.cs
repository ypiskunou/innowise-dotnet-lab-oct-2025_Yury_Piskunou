namespace Calculator.Engine;

using Core;

public class CalculatorEngine : ICalculatorSession
{
    private readonly ICalculationService _calculationService;
    
    public double CurrentValue { get; private set; }
    
    public CalculatorEngine(ICalculationService calculationService)
    {
        _calculationService = calculationService;
        CurrentValue = 0;
    }

    public void ExecuteExpression(string expression)
    {
        var result = _calculationService.Evaluate(expression);
        
        CurrentValue = result;
    }

    public void UndoLast()
    {
        throw new NotImplementedException();
    }
}