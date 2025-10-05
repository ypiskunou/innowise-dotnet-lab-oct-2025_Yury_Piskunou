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
    public void Evaluate_ShouldReturnNumber_WhenExpressionIsSingleNumber()
    {
        ICalculationService service = new CalculationService(); 
        var expression = "123";
        
        var result = service.Evaluate(expression);
        
        Assert.Equal(123, result);
    }
    
    [Fact]
    public void Evaluate_ShouldReturnCorrectSum_ForSimpleAddition()
    {
        ICalculationService service = new CalculationService();
        var expression = "2+3";
        
        var result = service.Evaluate(expression);
        
        Assert.Equal(5, result);
    }
}