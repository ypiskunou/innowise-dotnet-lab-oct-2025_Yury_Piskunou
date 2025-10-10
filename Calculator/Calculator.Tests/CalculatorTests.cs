using Calculator.Operations;

namespace Calculator.Tests;

public class CalculatorTests
{
    private readonly ICalculator _calculator; 

    public CalculatorTests()
    {
        var operations = new List<IOperation>
        {
            new Addition(), new Subtraction(), new Multiplication(), new Division()
        };
        _calculator = new Calculator(operations);
    }

    [Fact]
    public void Calculate_WhenDividingByZero_ShouldThrowDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() =>
            _calculator.Calculate(10, '/', 0));
    }
}