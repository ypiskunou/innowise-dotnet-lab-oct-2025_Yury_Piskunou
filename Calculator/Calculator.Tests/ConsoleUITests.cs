namespace Calculator.Tests;

using Xunit;
using Moq;

public class ConsoleUITests
{
    private readonly Mock<ICalculator> _mockCalculator;
    private readonly TestConsole _testConsole;
    private readonly ConsoleUI _consoleUI;

    public ConsoleUITests()
    {
        _mockCalculator = new Mock<ICalculator>();
        _testConsole = new TestConsole();

        _consoleUI = new ConsoleUI(_mockCalculator.Object, "+-*/", _testConsole);
    }

    [Theory]
    [InlineData("123", 123)]
    [InlineData(" 45.5 ", 45.5)] 
    [InlineData("9,1", 9.1)] 
    [InlineData("-10", -10)] 
    public void ReadDouble_WithValidInput_ReturnsCorrectNumber(string input, double expected)
    {
        var testConsole = new TestConsole();
        var ui = new ConsoleUI(null, "", testConsole);
        
        testConsole.FeedInput(input);
        
        double result = ui.ReadDouble("Test prompt");
        
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("abc")] 
    [InlineData("7 8")]
    [InlineData("12..3")]
    public void ReadDouble_WithInvalidInput_Reprompts(string invalidInput)
    {
        var testConsole = new TestConsole();
        var ui = new ConsoleUI(null, "", testConsole);
        
        testConsole.FeedInput(invalidInput, "100");
        
        double result = ui.ReadDouble("Test prompt");
        
        Assert.Contains("Ошибка: введено некорректное число", testConsole.FullOutput);
        Assert.Equal(100, result);
    }

    [Theory]
    [InlineData("42", "8", 42, 8)] 
    [InlineData(" 9,1 ", " 0.9 ", 9.1, 0.9)] 
    public void Start_WithVariousValidInputs_PerformsCorrectCalculation(
        string firstInput, string secondInput, double expectedFirstNum, double expectedSecondNum)
    {
        double expectedResult = expectedFirstNum + expectedSecondNum;
        
        _testConsole.FeedInput(firstInput, "+", secondInput, "n");
        
        _mockCalculator
            .Setup(c => c.Calculate(expectedFirstNum, '+', expectedSecondNum))
            .Returns(expectedResult);
        
        _consoleUI.Start();
        
        Assert.Contains($"Результат: {expectedResult}", _testConsole.FullOutput);
    }
}