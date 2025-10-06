using Calculator.Core.Strategies;

namespace Calculator.Engine;

using Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class SimpleCalculationService : ICalculationService
{
    private readonly Dictionary<char, IOperationStrategy> _strategies;

    public SimpleCalculationService()
    {
        _strategies = new IOperationStrategy[]
        {
            new AdditionStrategy(),
            new SubtractionStrategy(),
            new MultiplicationStrategy(),
            new DivisionStrategy()
        }.ToDictionary(s => s.OperatorSymbol);
    }
    
    public double Evaluate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentException("Выражение не может быть пустым.");
        
        var (leftOperand, operation, rightOperand) = ParseExpression(expression.Trim());
        
        if (!_strategies.TryGetValue(operation, out var strategy))
        {
            throw new InvalidOperationException($"Операция '{operation}' не поддерживается.");
        }
        
        return strategy.Execute(leftOperand, rightOperand);
    }
    
    private (double left, char operation, double right) ParseExpression(string expression)
    {
        var operatorsToSearch = _strategies.Keys.Where(op => op != '-').ToArray();
        int operatorIndex = expression.IndexOfAny(operatorsToSearch, 1);
    
        if (operatorIndex == -1)
        {
            int minusIndex = expression.IndexOf('-', 1);
            if (minusIndex > 0) operatorIndex = minusIndex;
        }

        if (operatorIndex == -1)
            throw new ArgumentException("Неверный формат выражения: оператор не найден.");

        char operation = expression[operatorIndex];
    
        string leftStr = expression.Substring(0, operatorIndex);
        string rightStr = expression.Substring(operatorIndex + 1);

        if (!double.TryParse(leftStr.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double left))
            throw new ArgumentException($"Неверный формат левого операнда: '{leftStr.Trim()}'");

        if (!double.TryParse(rightStr.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double right))
            throw new ArgumentException($"Неверный формат правого операнда: '{rightStr.Trim()}'");

        return (left, operation, right);
    }
}