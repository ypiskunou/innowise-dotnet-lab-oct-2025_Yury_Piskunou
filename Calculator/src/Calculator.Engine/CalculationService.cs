namespace Calculator.Engine;

using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

public class CalculationService : ICalculationService
{
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
        var tokens = new List<Token>();
        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];

            if (char.IsWhiteSpace(c))
            {
                continue;
            }

            Token? lastToken = tokens.LastOrDefault();
            Token? newToken = null;

            if (IsStartOfNumber(c, i, expression, lastToken))
            {
                newToken = ReadNumberToken(expression, ref i);
            }
            else if ("+-*/()".Contains(c))
            {
                newToken = ReadOperatorOrParenthesisToken(c);
            }

            if (newToken != null)
            {
                tokens.Add(newToken);
            }
            else
            {
                throw new ArgumentException($"Недопустимый символ в выражении: '{c}'");
            }
        }
        return tokens;
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
                {
                    throw new ArgumentException($"Недопустимое использование оператора '{current.Lexeme}' после '{prev?.Lexeme}'.");
                }
            }
            
            if (current.Type == TokenType.Number)
            {
                if (prev is { Type: TokenType.RightParenthesis })
                {
                    throw new ArgumentException($"Неожиданное число '{current.Lexeme}' после закрывающей скобки.");
                }
            }
        }
    }
    
    private bool IsStartOfNumber(char c, int index, string expression, Token? lastToken)
    {
        if (char.IsDigit(c) || c == '.')
        {
            return true;
        }
        
        if (c == '-')
        {
            bool isUnary = lastToken is null ||
                           lastToken is { Type: TokenType.Operator } ||
                           lastToken is { Type: TokenType.LeftParenthesis };
            return isUnary;
        }
        return false;
    }
    
    private Token ReadNumberToken(string expression, ref int index)
    {
        string numberStr = "";
        
        if(expression[index] == '-')
        {
            numberStr += "-";
            index++;
        }

        while (index < expression.Length && (char.IsDigit(expression[index]) || expression[index] == '.'))
        {
            numberStr += expression[index];
            index++;
        }
        index--;

        if(!double.TryParse(numberStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
        {
             throw new ArgumentException($"Некорректный формат числа: '{numberStr}'");
        }
        
        return new Token(TokenType.Number, numberStr, value);
    }
    
    private Token ReadOperatorOrParenthesisToken(char c)
    {
        return c switch
        {
            '(' => new Token(TokenType.LeftParenthesis, c.ToString()),
            ')' => new Token(TokenType.RightParenthesis, c.ToString()),
            _ => new Token(TokenType.Operator, c.ToString()),
        };
    }

    private List<Token> ConvertToPostfix(List<Token> infixTokens)
    {
        var output = new Queue<Token>();
        var operators = new Stack<Token>();

        foreach (var token in infixTokens)
        {
            if (token.Type == TokenType.Number)
            {
                output.Enqueue(token);
            }
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
            else if (token.Type == TokenType.LeftParenthesis)
            {
                operators.Push(token);
            }
            else if (token.Type == TokenType.RightParenthesis)
            {
                while (operators.Count > 0 && operators.Peek().Type != TokenType.LeftParenthesis)
                {
                    output.Enqueue(operators.Pop());
                }
                if (operators.Count == 0 || operators.Peek().Type != TokenType.LeftParenthesis)
                {
                    throw new ArgumentException("Несогласованные скобки.");
                }
                operators.Pop();
            }
        }

        while (operators.Count > 0)
        {
            var op = operators.Pop();
            if (op.Type == TokenType.LeftParenthesis || op.Type == TokenType.RightParenthesis)
            {
                throw new ArgumentException("Несогласованные скобки.");
            }
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
                if (token.Value.HasValue)
                {
                    stack.Push(token.Value.Value);
                }
                else
                {
                    throw new InvalidOperationException($"Токен-число не имеет значения: {token.Lexeme}");
                }
            }
            else if (token.Type == TokenType.Operator)
            {
                if (stack.Count < 2) throw new ArgumentException("Неверный синтаксис выражения (недостаточно операндов).");

                var right = stack.Pop();
                var left = stack.Pop();
                double result = 0;

                switch (token.Lexeme)
                {
                    case "+": result = left + right; break;
                    case "-": result = left - right; break;
                    case "*": result = left * right; break;
                    case "/":
                        if (right == 0) throw new DivideByZeroException("Деление на ноль.");
                        result = left / right;
                        break;
                }
                stack.Push(result);
            }
        }

        if (stack.Count != 1) throw new ArgumentException("Неверный синтаксис выражения (в стеке осталось больше одного значения).");

        return stack.Pop();
    }
}