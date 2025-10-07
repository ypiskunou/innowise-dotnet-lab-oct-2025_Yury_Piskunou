namespace Calculator.Engine.Tests;

using System;
using Xunit;
using Core;
using Engine;

public class SimpleCalculationServiceTests
{
    private readonly ICalculationService _service = new SimpleCalculationServiceWithRegex();

    [Theory]
    [InlineData("5+10", 15)]
    [InlineData("-5+10", 5)]
    [InlineData("2+-5", -3)]
    [InlineData("-2+-5", -7)]
    [InlineData("0+0", 0)]
    [InlineData(" 1.5 + 2.5 ", 4.0)] // Проверяем пробелы и числа с плавающей точкой
    public void Evaluate_Should_ReturnCorrectSum_ForVariousInputs(string expression, double expected)
    {
        var result = _service.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("10-5", 5)]
    [InlineData("5-10", -5)]
    [InlineData("-5-10", -15)]
    [InlineData("10--5", 15)]
    public void Evaluate_Should_ReturnCorrectDifference_ForVariousInputs(string expression, double expected)
    {
        var result = _service.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("-2*7", -14)]
    [InlineData("-2*3", -6)]
    [InlineData("2*-3", -6)]
    [InlineData("-2*-3", 6)]
    public void Evaluate_Should_ReturnCorrectProduct_ForVariousInputs(string expression, double expected)
    {
        var result = _service.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("10/2", 5)]
    [InlineData("-10/2", -5)]
    [InlineData("10/-2", -5)]
    [InlineData("-10/-2", 5)]
    public void Evaluate_Should_ReturnCorrectQuotient_ForVariousInputs(string expression, double expected)
    {
        var result = _service.Evaluate(expression);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Evaluate_Should_ThrowDivideByZeroException_WhenDividingByZero()
    {
        Assert.Throws<DivideByZeroException>(() => _service.Evaluate("5 / 0"));
    }

    [Theory]
    [InlineData("2+3+4")] 
    [InlineData("5")] 
    [InlineData("10 5")] 
    [InlineData("")]
    [InlineData("  ")] 
    [InlineData("a+b")]
    [InlineData("*5")] 
    [InlineData("5+")] 
    [InlineData("5-")]
    [InlineData("5- 5 5")]
    public void Evaluate_Should_ThrowArgumentException_ForInvalidExpressions(string invalidExpression)
    {
        Assert.Throws<ArgumentException>(() => _service.Evaluate(invalidExpression));
    }
}