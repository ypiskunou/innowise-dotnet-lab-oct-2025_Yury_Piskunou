namespace Calculator.Engine;

using Core;
using Core.Strategies;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public class SimpleCalculationServiceWithRegex : ICalculationService
{
    private readonly Dictionary<char, IOperationStrategy> _strategies;

    private static readonly Regex ExpressionRegex = new Regex(
        @"^\s*(?<left>-?\d+(\.\d+)?)\s*(?<op>[+\-*/])\s*(?<right>-?\d+(\.\d+)?)\s*$",
        RegexOptions.Compiled | RegexOptions.ExplicitCapture
    );
    
    public SimpleCalculationServiceWithRegex()
    {
        var strategyImplementations = new IOperationStrategy[]
        {
            new AdditionStrategy(),
            new SubtractionStrategy(),
            new MultiplicationStrategy(),
            new DivisionStrategy()
        };
        _strategies = strategyImplementations.ToDictionary(s => s.OperatorSymbol);
    }

    public double Evaluate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentException("Выражение не может быть пустым.");

        var match = ExpressionRegex.Match(expression);

        if (!match.Success)
            throw new ArgumentException("Неверный формат выражения. Ожидается 'число оператор число'.");
        
        string leftStr = match.Groups["left"].Value;
        string opStr = match.Groups["op"].Value;
        string rightStr = match.Groups["right"].Value;
        
        if (!double.TryParse(leftStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double left))
            throw new ArgumentException($"Неверный формат левого операнда: '{leftStr}'");
        
        if (!double.TryParse(rightStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double right))
            throw new ArgumentException($"Неверный формат правого операнда: '{rightStr}'");

        char operation = opStr[0];

        if (!_strategies.TryGetValue(operation, out var strategy))
            throw new InvalidOperationException($"Операция '{operation}' не поддерживается.");
        
        return strategy.Execute(left, right);
    }
}