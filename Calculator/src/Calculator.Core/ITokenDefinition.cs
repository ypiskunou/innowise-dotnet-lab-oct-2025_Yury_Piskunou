using System.Text.RegularExpressions;

namespace Calculator.Core;

public interface ITokenDefinition
{
    string TokenTypeKey { get; }
    string Pattern { get; }
    Token CreateToken(Match match);
}