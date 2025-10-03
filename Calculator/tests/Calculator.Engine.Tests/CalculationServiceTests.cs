namespace Calculator.Engine.Tests;

using Xunit;
using Calculator.Core;
using Calculator.Engine; 

public class CalculationServiceTests
{
    [Fact]
    public void Evaluate_ShouldReturnNumber_WhenExpressionIsSingleNumber()
    {
        ICalculationService service = new CalculationService(); 
        var expression = "123";
        
        var result = service.Evaluate(expression);
        
        Assert.Equal(123, result);
    }
}