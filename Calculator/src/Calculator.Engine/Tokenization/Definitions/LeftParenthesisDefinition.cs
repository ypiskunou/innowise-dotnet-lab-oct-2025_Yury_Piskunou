namespace Calculator.Engine.Tokenization.Definitions;

using Core;
using System.Text.RegularExpressions;

public class LeftParenthesisDefinition : ITokenDefinition
{
    public string TokenTypeKey => "LeftParen";
    public string Pattern => @"\(";

    public Token CreateToken(Match match)
    {
        return new Token(TokenType.LeftParenthesis, match.Value);
    }
}