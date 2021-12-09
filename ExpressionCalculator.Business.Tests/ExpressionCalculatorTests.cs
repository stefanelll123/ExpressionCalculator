using NUnit.Framework;

namespace ExpressionCalculator.Business.Tests
{
    public class ExpressionCalculatorTests
    {
        [Test]
        [TestCase("6+5+[3-20*(5+7)]", -226)]
        [TestCase("5+6", 11)]
        [TestCase("5", 5)]
        [TestCase("(5+7)", 12)]
        [TestCase("3*9/7", 3)]
        [TestCase("(5+7)+3", 15)]
        [TestCase("(5+7)+3+{2+[3*9/7+(1/2)]}", 20)]
        [TestCase("([1+2])", 3)]
        public void WhenCalculateForExpression_WithValidInput_ShouldReturnCorrectValue(string input, int expected)
        {
            // Arrange
            var calculator = new ApplicationService.ExpressionCalculator();

            // Act
            var result = calculator.Calculate(input);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}