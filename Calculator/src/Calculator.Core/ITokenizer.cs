namespace Calculator.Core;

public interface ITokenizer
{
    List<Token> Tokenize(string expression);
}