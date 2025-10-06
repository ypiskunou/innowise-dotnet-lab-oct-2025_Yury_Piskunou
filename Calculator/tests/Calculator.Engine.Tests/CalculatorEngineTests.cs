namespace Calculator.Engine.Tests;

using Xunit;
using Calculator.Core;
using Calculator.Engine;

public class CalculatorEngineTests
{
    [Fact]
    public void ExecuteExpression_Should_UpdateCurrentValue_WithCalculationResult()
    {
        ICalculationService calculationService = new CalculationService();
        ICalculatorSession session = new CalculatorEngine(calculationService);
        var expression = "2+3*4"; 
        
        session.ExecuteExpression(expression);
        
        Assert.Equal(14, session.CurrentValue);
    }
    
    // В CalculatorEngineTests.cs

    [Fact]
    public void UndoLast_Should_RevertToPreviousValue_AfterOneOperation()
    {
        ICalculationService calculationService = new CalculationService();
        ICalculatorSession session = new CalculatorEngine(calculationService);

        session.ExecuteExpression("10+5");
        var valueAfterFirstExecution = session.CurrentValue;
        
        session.UndoLast();
        
        Assert.Equal(15, valueAfterFirstExecution);
        Assert.Equal(0, session.CurrentValue); 
    }

    [Fact]
    public void UndoLast_Should_DoNothing_WhenHistoryIsEmpty()
    {
        ICalculationService calculationService = new CalculationService();
        ICalculatorSession session = new CalculatorEngine(calculationService);

        var exception = Record.Exception(() => session.UndoLast());
        
        Assert.Null(exception); 
        Assert.Equal(0, session.CurrentValue);
    }
}