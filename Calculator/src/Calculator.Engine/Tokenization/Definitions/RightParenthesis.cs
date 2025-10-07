namespace Calculator.Engine.Tokenization.Definitions;

using Core;
using System.Text.RegularExpressions;

public class RightParenthesisDefinition : ITokenDefinition
{
    public string TokenTypeKey => "RightParen";
    public string Pattern => @"\)";

    public Token CreateToken(Match match)
    {
        return new Token(TokenType.RightParenthesis, match.Value);
    }
}