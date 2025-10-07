using Calculator.Core.Strategies;
using Calculator.Engine.Tokenization.Definitions;

namespace Calculator.ConsoleApp;

using Core;
using Engine;

public class Program
{
    public static void Main(string[] args)
    {
        var tokenDefinitions = new List<ITokenDefinition>
        {
            new NumberDefinition(),
            new OperatorDefinition(),
            new LeftParenthesisDefinition(),
            new RightParenthesisDefinition()
        };

        var strategies = new IOperationStrategy[]
        {
            new AdditionStrategy(),
            new SubtractionStrategy(),
            new MultiplicationStrategy(),
            new DivisionStrategy()
        };
        
        var tokenizer = new RegexTokenizer(tokenDefinitions);
        
        var calculationService = new CalculationServiceWithRegex(tokenizer, strategies);
        
        var session = new CalculatorEngine(calculationService);
        
        IApplicationHost host = new ConsoleUI(session);
        host.Run();
    }
}