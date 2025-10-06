namespace Calculator.Engine.Tests;

using Xunit;
using Calculator.Core;
using Calculator.Engine;

public class CalculationServiceTests
{
    private readonly ICalculationService _service;

    public CalculationServiceTests()
    {
        _service = new CalculationService();
    }
    
    [Fact]
    public void Evaluate_Should_ReturnNumber_WhenExpressionIsSingleNumber()
    {
        var result = _service.Evaluate("123");
        
        Assert.Equal(123, result);
    }


    [Theory]
    [InlineData("5+10", 15)]
    [InlineData("-5+10", 5)]
    [InlineData("0+0", 0)]
    public void Evaluate_Should_ReturnCorrectSum_ForVariousAdditions(string expression, double expected)
    {
        var result = _service.Evaluate(expression);
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("10-5", 5)]
    [InlineData("5-10", -5)]
    [InlineData("0-5", -5)]
    public void Evaluate_Should_ReturnCorrectDifference_ForVariousSubtractions(string expression, double expected)
    {
        var result = _service.Evaluate(expression);
        
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void Evaluate_Should_RespectOperatorPrecedence_ForMixedOperations()
    {
        var expression = "2+3*4"; 
        
        var result = _service.Evaluate(expression);
        
        Assert.Equal(14, result);
    }

    [Fact]
    public void Evaluate_Should_RespectOperatorPrecedence_WithMultipleMixedOperations()
    {
        var expression = "10-2*3+5"; 
        
        var result = _service.Evaluate(expression);
        
        Assert.Equal(9, result);
    }
}