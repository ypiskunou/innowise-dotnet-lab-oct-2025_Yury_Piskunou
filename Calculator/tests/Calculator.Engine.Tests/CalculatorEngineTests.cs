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
}