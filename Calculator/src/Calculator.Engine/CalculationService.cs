namespace Calculator.Engine;

using Calculator.Core;
using System.Collections.Generic;
using System.Linq;

public class CalculationService : ICalculationService
{
    public double Evaluate(string expression)
    {
        var infixTokens = Tokenize(expression);
        
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
                continue;

            if (char.IsDigit(c))
            {
                var numberStr = c.ToString();
                
                while (i + 1 < expression.Length && char.IsDigit(expression[i + 1]))
                {
                    numberStr += expression[i + 1];
                    i++;
                }
                
                tokens.Add(new Token(TokenType.Number, numberStr, double.Parse(numberStr)));
            }
            else if ("+-*/".Contains(c))
            {
                tokens.Add(new Token(TokenType.Operator, c.ToString()));
            }
        }
        return tokens;
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
                while (operators.Count > 0)
                {
                    output.Enqueue(operators.Pop());
                }
                operators.Push(token);
            }
        }

        while (operators.Count > 0)
        {
            output.Enqueue(operators.Pop());
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
                stack.Push(token.Value.Value);
            }
            else if (token.Type == TokenType.Operator)
            {
                var right = stack.Pop();
                var left = stack.Pop();
                double result = 0;

                switch (token.Lexeme)
                {
                    case "+": result = left + right; break;
                    case "-": result = left - right; break;
                    case "*": result = left * right; break;
                    case "/": result = left / right; break;
                }
                stack.Push(result);
            }
        }
        return stack.Pop();
    }
}