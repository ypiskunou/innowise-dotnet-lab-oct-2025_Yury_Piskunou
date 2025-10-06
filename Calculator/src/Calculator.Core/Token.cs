namespace Calculator.Core;

public record Token(TokenType Type, string Lexeme, double? Value = null)
{
    public int Precedence
    {
        get
        {
            if (Type != TokenType.Operator) return 0;

            return Lexeme switch
            {
                "+" => 1,
                "-" => 1,
                "*" => 2,
                "/" => 2,
                _ => 0 
            };
        }
    }
    
    public bool IsLeftAssociative => Type == TokenType.Operator && "+-*/".Contains(Lexeme);
}