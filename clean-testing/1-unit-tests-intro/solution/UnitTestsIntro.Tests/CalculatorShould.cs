using System;
using NUnit.Framework;

namespace UnitTestsIntro.Tests
{
    public class CalculatorShould
    {
        [Test]
        public void SupportAdd()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Calculate(9, 3, Calculator.Add);

            // Assert
            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        public void SupportMultiply()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Calculate(3,76, Calculator.Multiply);

            // Assert
            Assert.That(result, Is.EqualTo(228));

        }
    
        [Test]
        public void SupportDivide()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Calculate(9,3, Calculator.Divide);

            // Assert
            Assert.That(result, Is.EqualTo(3));
        }
    
        [Test]
        public void SupportSubtract()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Calculate(9,3, Calculator.Subtract);

            // Assert
            Assert.That(result, Is.EqualTo(6));
        }
    
        [Test]
        public void FailWhenOperatorNotSupported()
        {
            var calculator = new Calculator();
            var exception = Assert.Throws<ArgumentException>(() => calculator.Calculate(9, 3, "UnsupportedOperator"));
            Assert.That(exception!.Message, Is.EqualTo("Not supported operator"));
        }
    }
}