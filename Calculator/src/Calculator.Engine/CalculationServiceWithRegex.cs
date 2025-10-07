using Calculator.Core.Strategies;

namespace Calculator.Engine;

using Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

public class CalculationServiceWithRegex : ICalculationService
{
    private readonly Dictionary<string, IOperationStrategy> _strategies;

    private static readonly Regex TokenizerRegex = new Regex(
        @"
            (?<Number>      \d+(\.\d+)?) |
            (?<Operator>    [+\-*/])       |
            (?<LeftParen>   \()            |
            (?<RightParen>  \))            |
            (?<Whitespace>  \s+)           |
            (?<Mismatch>    .)
            ",
        RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture
    );

    public CalculationServiceWithRegex()
    {
        _strategies = new IOperationStrategy[]
        {
            new AdditionStrategy(),
            new SubtractionStrategy(),
            new MultiplicationStrategy(),
            new DivisionStrategy()
        }.ToDictionary(s => s.OperatorSymbol.ToString());
    }

    public double Evaluate(string expression)
    {
        var infixTokens = Tokenize(expression);
        ValidateTokens(infixTokens);
        var postfixTokens = ConvertToPostfix(infixTokens);
        var result = EvaluatePostfix(postfixTokens);
        return result;
    }

    private List<Token> Tokenize(string expression)
    {
        var rawTokens = new List<Token>();
        var matches = TokenizerRegex.Matches(expression);

        foreach (Match match in matches)
        {
            if (match.Groups["Whitespace"].Success) continue;
            if (match.Groups["Number"].Success)
                rawTokens.Add(new Token(TokenType.Number, match.Value,
                    double.Parse(match.Value, CultureInfo.InvariantCulture)));
            else if (match.Groups["Operator"].Success)
                rawTokens.Add(new Token(TokenType.Operator, match.Value));
            else if (match.Groups["LeftParen"].Success)
                rawTokens.Add(new Token(TokenType.LeftParenthesis, match.Value));
            else if (match.Groups["RightParen"].Success)
                rawTokens.Add(new Token(TokenType.RightParenthesis, match.Value));
            else if (match.Groups["Mismatch"].Success)
                throw new ArgumentException($"Недопустимый символ в выражении: '{match.Value}'");
        }

        return PostProcessUnaryMinus(rawTokens);
    }

    private List<Token> PostProcessUnaryMinus(List<Token> rawTokens)
    {
        var processedTokens = new List<Token>();
        for (int i = 0; i < rawTokens.Count; i++)
        {
            var current = rawTokens[i];
            if (current.Type == TokenType.Operator && current.Lexeme == "-")
            {
                var prev = i > 0 ? processedTokens.LastOrDefault() : null;
                bool isUnary = prev is null || prev.Type == TokenType.Operator ||
                               prev.Type == TokenType.LeftParenthesis;

                if (isUnary && i + 1 < rawTokens.Count && rawTokens[i + 1].Type == TokenType.Number)
                {
                    var numberToken = rawTokens[i + 1];
                    processedTokens.Add(
                        new Token(TokenType.Number, "-" + numberToken.Lexeme, -numberToken.Value!.Value));
                    i++;
                }
                else
                {
                    processedTokens.Add(current);
                }
            }
            else
            {
                processedTokens.Add(current);
            }
        }

        return processedTokens;
    }

    private void ValidateTokens(List<Token> tokens)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            var current = tokens[i];
            var prev = i > 0 ? tokens[i - 1] : null;

            if (current.Type == TokenType.Operator)
            {
                if (prev is null || prev.Type == TokenType.LeftParenthesis || prev.Type == TokenType.Operator)
                    throw new ArgumentException(
                        $"Недопустимое использование оператора '{current.Lexeme}' после '{prev?.Lexeme}'.");
            }

            if (current.Type == TokenType.Number)
            {
                if (prev is { Type: TokenType.RightParenthesis })
                    throw new ArgumentException($"Неожиданное число '{current.Lexeme}' после закрывающей скобки.");
            }
        }
    }

    private List<Token> ConvertToPostfix(List<Token> infixTokens)
    {
        var output = new Queue<Token>();
        var operators = new Stack<Token>();

        foreach (var token in infixTokens)
        {
            if (token.Type == TokenType.Number) output.Enqueue(token);
            else if (token.Type == TokenType.Operator)
            {
                while (operators.Count > 0 && operators.Peek().Type == TokenType.Operator &&
                       ((token.IsLeftAssociative && token.Precedence <= operators.Peek().Precedence) ||
                        (!token.IsLeftAssociative && token.Precedence < operators.Peek().Precedence)))
                {
                    output.Enqueue(operators.Pop());
                }

                operators.Push(token);
            }
            else if (token.Type == TokenType.LeftParenthesis) operators.Push(token);
            else if (token.Type == TokenType.RightParenthesis)
            {
                while (operators.Count > 0 && operators.Peek().Type != TokenType.LeftParenthesis)
                {
                    output.Enqueue(operators.Pop());
                }

                if (operators.Count == 0 || operators.Peek().Type != TokenType.LeftParenthesis)
                    throw new ArgumentException("Несогласованные скобки.");
                operators.Pop();
            }
        }

        while (operators.Count > 0)
        {
            var op = operators.Pop();
            if (op.Type == TokenType.LeftParenthesis || op.Type == TokenType.RightParenthesis)
                throw new ArgumentException("Несогласованные скобки.");
            output.Enqueue(op);
        }

        return output.ToList();
    }

    private double EvaluatePostfix(List<Token> postfixTokens)
    {
        var stack = new Stack<double>();

        foreach (var token in postfixTokens)
        {
            if (token.Type == TokenType.Number)
            {
                if (token.Value.HasValue) stack.Push(token.Value.Value);
                else throw new InvalidOperationException($"Токен-число не имеет значения: {token.Lexeme}");
            }
            else if (token.Type == TokenType.Operator)
            {
                if (stack.Count < 2)
                    throw new ArgumentException("Неверный синтаксис выражения (недостаточно операндов).");

                var right = stack.Pop();
                var left = stack.Pop();

                if (!_strategies.TryGetValue(token.Lexeme, out var strategy))
                    throw new InvalidOperationException($"Операция '{token.Lexeme}' не поддерживается.");

                var result = strategy.Execute(left, right);
                stack.Push(result);
            }
        }

        if (stack.Count != 1)
            throw new ArgumentException("Неверный синтаксис выражения (в стеке осталось больше одного значения).");

        return stack.Pop();
    }
}