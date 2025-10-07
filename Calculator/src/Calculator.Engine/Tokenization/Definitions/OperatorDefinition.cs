namespace Calculator.Engine.Tokenization.Definitions;

using Core;
using System.Text.RegularExpressions;

public class OperatorDefinition : ITokenDefinition
{
    public string TokenTypeKey => "Operator";
    public string Pattern => @"[+\-*/]";

    public Token CreateToken(Match match)
    {
        return new Token(TokenType.Operator, match.Value);
    }
}