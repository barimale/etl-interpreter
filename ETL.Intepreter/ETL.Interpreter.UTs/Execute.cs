using ETL.Intepreter;
using ETL.Intepreter.Model.Parse;

namespace ETL.Interpreter.UTs
{
    public class Execute
    {
        [Fact]
        public void ExampleOfUsage()
        {
            // given
            var input = "(13+4)-(5+6)";

            // when
            var tokens = LexService.Lex(input);
            var ast = ParseService.Parse(tokens);
            var result = ast.Value;

            // then
            Assert.Equal(6, result);
        }

        [Fact]
        public void ExampleOfUsageWithMultiplication()
        {
            // given
            var input = "(13+4)-(5*6)";

            // when
            var tokens = LexService.Lex(input);
            var ast = ParseService.Parse(tokens);
            var result = ast.Value;

            // then
            Assert.Equal(-13, result);
        }

        [Fact]
        public void ExampleOfUsageWithMultiplication2()
        {
            // given
            var input = "(13*4)/(52*1)";

            // when
            var tokens = LexService.Lex(input);
            var ast = ParseService.Parse(tokens);
            var result = ast.Value;

            // then
            Assert.Equal(1, result);
        }

        [Fact]
        public void ExampleOfUsageWithMultiplication3()
        {
            // given
            var input = "2+2*2";

            // when
            var tokens = LexService.Lex(input);
            var ast = ParseService.Parse(tokens);
            var result = ast.Value;

            // then
            Assert.Equal(6, result);
        }

        [Fact]
        public void ExampleOfUsageWithMultiplication4()
        {
            // given
            var input = "-12+2*2";

            // when
            var tokens = LexService.Lex(input);
            var ast = ParseService.Parse(tokens);
            var result = ast.Value;

            // then
            Assert.Equal(-8, result);
        }

        [Fact]
        public void ExampleOfIncorrectUsage()
        {
            // given
            var input = "(13+4)-(5+6(";

            // then
            Assert.Throws<Exception>(() =>
            {
                // when
                var tokens = LexService.Lex(input);
                var ast = ParseService.Parse(tokens);
                var result = ast.Value;
            });
        }
    }
}
