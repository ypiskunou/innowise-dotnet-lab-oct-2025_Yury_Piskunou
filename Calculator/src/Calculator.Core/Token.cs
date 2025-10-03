namespace Calculator.Core;

public record Token(TokenType Type, string Lexeme, double? Value = null);