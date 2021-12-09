using System;
using ExpressionCalculator.Business.ApplicationService;
using NUnit.Framework;

namespace ExpressionCalculator.Business.Tests
{
    public class ExpressionFiniteAutomateTests
    {
        [Test]
        [TestCase("6+5+[3-20*(5+7)]")]
        [TestCase("5+6")]
        [TestCase("5")]
        [TestCase("(5+7)")]
        [TestCase("(5+7)+3")]
        [TestCase("(5+7)+3+{2+[3*9/7+(1/2)]}")]
        [TestCase("([1+2])")]
        public void WhenGenerateExpression_WithValidInput_ShouldReturnNode(string input)
        {
            // Arrange
            var parser = new ExpressionFiniteAutomate();
            
            // Act
            var result = parser.CreateExpressionTree(input);
            
            // Assert
            Assert.IsNotNull(result);
        }
        
        [Test]
        [TestCase("([1+2])}")]
        [TestCase("{{([1+2])}")]
        [TestCase("6+5[3-20*(5+7)]")]
        [TestCase("([])")]
        [TestCase("([a])")]
        public void WhenGenerateExpression_WithInvalidInput_ShouldThrowException(string input)
        {
            // Arrange
            var parser = new ExpressionFiniteAutomate();
            
            // Act + Assert
            Assert.Throws<Exception>(() =>  parser.CreateExpressionTree(input));
        }
    }
}