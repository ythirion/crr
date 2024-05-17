namespace UnitTestsIntro
{
    public class Calculator
    {
        private const string Add = "add";
        private const string Multiply = "multiply";
        private const string Divide = "divide";
        private const string Subtract = "subtract";

        private static readonly Dictionary<string, Func<int, int, int>> SupportedOperators =
            new()
            {
                {Add, (a, b) => a + b},
                {Multiply, (a, b) => a * b},
                {Divide, (a, b) => a / b},
                {Subtract, (a, b) => a - b}
            };
        
        public int Calculate(int a, int b, string @operator) =>
            SupportedOperators.ContainsKey(@operator)
                ? SupportedOperators[@operator](a, b)
                : throw new ArgumentException("Not supported operator");
    }
}