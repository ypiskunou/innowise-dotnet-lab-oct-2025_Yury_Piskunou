using System.Text.RegularExpressions;
using Calculator.Core;

namespace Calculator.Engine;

 public class RegexTokenizer: ITokenizer
    {
        private readonly Regex _masterRegex;
        private readonly Dictionary<string, ITokenDefinition> _definitionMap;
        
        private const string WhitespaceKey = "Whitespace";
        private const string MismatchKey = "Mismatch";

        public RegexTokenizer(IEnumerable<ITokenDefinition> definitions)
        {
            _definitionMap = definitions.ToDictionary(d => d.TokenTypeKey);
            
            var masterPattern = string.Join(" | ", definitions
                .Select(d => $"(?<{d.TokenTypeKey}>{d.Pattern})"));
            
            masterPattern += $" | (?<{WhitespaceKey}>\\s+) | (?<{MismatchKey}>.)";

            _masterRegex = new Regex(masterPattern, 
                RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture);
        }

        public List<Token> Tokenize(string expression)
        {
            var rawTokens = GenerateRawTokens(expression);
            var processedTokens = PostProcessUnaryMinus(rawTokens);
            return processedTokens;
        }

        private List<Token> GenerateRawTokens(string expression)
        {
            var tokens = new List<Token>();
            var matches = _masterRegex.Matches(expression);

            foreach (Match match in matches)
            {
                if (match.Groups[WhitespaceKey].Success)
                {
                    continue;
                }
                if (match.Groups[MismatchKey].Success)
                {
                    throw new ArgumentException($"Недопустимый символ в выражении: '{match.Value}'");
                }
                
                var successGroup = _definitionMap.Keys
                    .Select(key => match.Groups[key])
                    .FirstOrDefault(g => g.Success);

                if (successGroup != null)
                {
                    var definition = _definitionMap[successGroup.Name];
                    tokens.Add(definition.CreateToken(match));
                }
            }
            return tokens;
        }
        
        private List<Token> PostProcessUnaryMinus(List<Token> rawTokens)
        {
            var processedTokens = new List<Token>();
            for (int i = 0; i < rawTokens.Count; i++)
            {
                var current = rawTokens[i];
                if (current.Type == TokenType.Operator && current.Lexeme == "-")
                {
                    var prev = processedTokens.LastOrDefault();
                    bool isUnary = prev is null || 
                                   prev.Type == TokenType.Operator ||
                                   prev.Type == TokenType.LeftParenthesis;

                    if (isUnary && i + 1 < rawTokens.Count && rawTokens[i + 1].Type == TokenType.Number)
                    {
                        var numberToken = rawTokens[i + 1];
                        processedTokens.Add(new Token(TokenType.Number, "-" + numberToken.Lexeme, -numberToken.Value!.Value));
                        i++;
                    }
                    else
                    {
                        processedTokens.Add(current);
                    }
                }
                else
                {
                    processedTokens.Add(current);
                }
            }
            return processedTokens;
        }
    }