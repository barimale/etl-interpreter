using ETL.Intepreter.Model.Lex;
using ETL.Intepreter.Model.Parse.Operations;

namespace ETL.Intepreter.Model.Parse
{
    public class ParseService
    {
        public static IElement Parse(IReadOnlyList<Token> tokens)
        {
            int index = 0;
            return ParseExpression(tokens, ref index);
        }

        private static IElement ParseExpression(IReadOnlyList<Token> tokens, ref int index)
        {
            var left = ParsePrimary(tokens, ref index);

            while (index < tokens.Count)
            {
                var token = tokens[index];

                if (token.MyType != Token.Type.Plus &&
                    token.MyType != Token.Type.Minus &&
                    token.MyType != Token.Type.Multiply &&
                    token.MyType != Token.Type.Divide)
                    break;

                index++; // consume operator

                var op = new BinaryOperation
                {
                    Left = left,
                    MyType = MapTokenToType(token)
                };

                var right = ParsePrimary(tokens, ref index);
                op.Right = right;

                left = op; // left-associative
            }

            return left;
        }

        private static BinaryOperation.Type MapTokenToType(Token token)
        {
            return token.MyType == Token.Type.Plus
                                    ? BinaryOperation.Type.Addition
                                    : token.MyType == Token.Type.Minus
                                        ? BinaryOperation.Type.Substraction
                                        : token.MyType == Token.Type.Multiply
                                            ? BinaryOperation.Type.Multiplication
                                            : BinaryOperation.Type.Division;
        }

        private static IElement ParsePrimary(IReadOnlyList<Token> tokens, ref int index)
        {
            var token = tokens[index];

            switch (token.MyType)
            {
                case Token.Type.Integer:
                    index++;
                    return new Double(int.Parse(token.Text));

                case Token.Type.LParen:
                    index++; // consume '('
                    var expr = ParseExpression(tokens, ref index);

                    if (tokens[index].MyType != Token.Type.RParen)
                        throw new Exception("Missing closing parenthesis");

                    index++; // consume ')'
                    return expr;

                default:
                    throw new Exception($"Unexpected token: {token.MyType}");
            }
        }
    }
}
