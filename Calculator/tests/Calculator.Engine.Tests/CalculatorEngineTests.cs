namespace Calculator.Engine.Tests;

using Xunit;
using Core;
using Engine;

public class CalculatorEngineTests
{
    private readonly ICalculatorSession _session;
    
    public CalculatorEngineTests()
    {
        ICalculationService calculationService = new CalculationService();
        _session = new CalculatorEngine(calculationService);
    }

    [Fact]
    public void ExecuteExpression_Should_UpdateCurrentValue_WithCalculationResult()
    {
        var expression = "2+3*4";
        
        _session.ExecuteExpression(expression);

        Assert.Equal(14, _session.CurrentValue);
    }

    [Fact]
    public void UndoLast_Should_RevertToPreviousValue_AfterOneOperation()
    {
        _session.ExecuteExpression("10+5");
        var valueAfterFirstExecution = _session.CurrentValue;
        
        _session.UndoLast();
        
        Assert.Equal(15, valueAfterFirstExecution);
        Assert.Equal(0, _session.CurrentValue);
    }

    [Fact]
    public void UndoLast_Should_DoNothing_WhenHistoryIsEmpty()
    {
        var exception = Record.Exception(() => _session.UndoLast());
        
        Assert.Null(exception);
        Assert.Equal(0, _session.CurrentValue);
    }
    
    [Fact]
    public void UndoLast_Should_RevertSequentially_AfterMultipleOperations()
    {
        _session.ExecuteExpression("10+5");
        _session.ExecuteExpression("3*3");
        
        _session.UndoLast();
        var valueAfterFirstUndo = _session.CurrentValue;

        _session.UndoLast();
        var valueAfterSecondUndo = _session.CurrentValue;
        
        Assert.Equal(15, valueAfterFirstUndo);
        Assert.Equal(0, valueAfterSecondUndo);
    }
}