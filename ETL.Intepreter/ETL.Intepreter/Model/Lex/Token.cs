namespace ETL.Intepreter.Model.Lex
{
    public class Token
    {
        public Token.Type MyType;
        public string Text;

        public enum Type
        {
            Integer, Float, Plus, Minus, LParen, RParen, Multiply,Divide
        }

        public Token(Token.Type type, string text)
        {
            MyType = type;
            Text = text;
        }

        public override string ToString()
        {
            return $"`{Text}`";
        }
    }
}
