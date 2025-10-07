namespace Calculator.Engine.Tokenization.Definitions;

using Core;
using System.Globalization;
using System.Text.RegularExpressions;

public class NumberDefinition : ITokenDefinition
{
    public string TokenTypeKey => "Number";
    public string Pattern => @"\d+(\.\d+)?";

    public Token CreateToken(Match match)
    {
        if (!double.TryParse(match.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
        {
            throw new InvalidOperationException($"Не удалось преобразовать '{match.Value}' в число.");
        }

        return new Token(TokenType.Number, match.Value, value);
    }
}