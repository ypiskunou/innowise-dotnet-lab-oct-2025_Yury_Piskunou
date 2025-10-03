namespace Calculator.Core;

public class Token
{
    public TokenType Type { get; }
    public string Lexeme { get; } // Исходная строка, например "+", "123", "("
    public double? Value { get; } // Только для чисел

    // Конструктор, чтобы сделать объект неизменяемым (immutable)
    public Token(TokenType type, string lexeme, double? value = null)
    {
        Type = type;
        Lexeme = lexeme;
        Value = value;
    }
}