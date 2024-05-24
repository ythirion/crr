using System;
using FluentAssertions;
using NUnit.Framework;
using static UnitTestsIntro.Calculator;

namespace UnitTestsIntro.Tests
{
    public class RefactoredCalculatorShould
    {
        private readonly Calculator _calculator = new();

        [TestCase(9, 3, Add, 12)]
        [TestCase(3, 76, Multiply, 228)]
        [TestCase(9, 3, Divide, 3)]
        [TestCase(9, 3, Subtract, 6)]
        public void SupportOperations(int a, int b, string @operator, int expectedResult) =>
            _calculator
                .Calculate(a, b, @operator)
                .Should()
                .Be(expectedResult);
    
        [Test]
        public void FailWhenOperatorNotSupported() =>
            _calculator.Invoking(_ => _.Calculate(9, 3, "UnsupportedOperator"))
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Not supported operator");
    }
}