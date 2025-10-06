namespace Calculator.Engine;

using Core;
using System.Collections.Generic;

public class CalculatorEngine : ICalculatorSession
{
    private readonly ICalculationService _calculationService;
    
    private readonly Stack<ICommand> _history = new Stack<ICommand>();

    public double CurrentValue { get; private set; }

    public CalculatorEngine(ICalculationService calculationService)
    {
        _calculationService = calculationService;
        CurrentValue = 0;
    }

    public void ExecuteExpression(string expression)
    {
        var previousValue = CurrentValue;
        
        var result = _calculationService.Evaluate(expression);
        CurrentValue = result;

        var command = new CalculationCommand(previousValue, result);
        
        _history.Push(command);
    }

    public void UndoLast()
    {
        if (_history.Count > 0)
        {
            var command = _history.Pop();
            
            CurrentValue = command.UnExecute();
        }
    }
}