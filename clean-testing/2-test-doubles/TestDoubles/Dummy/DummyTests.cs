using System;
using FluentAssertions;
using NUnit.Framework;

namespace TestDoubles.Dummy
{
    public class DummyTests
    {
        private readonly Calculator _calculator = new();

        [Test]
        public void Should_Throw_An_ArgumentException_When_Divide_By_Zero()
        {
            const int numerator = 9;
            const int zero = 0;

            _calculator.Invoking(_ => _.Divide(numerator, zero))
                .Should()
                .Throw<ArgumentException>();
        }
    }
}