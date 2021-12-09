using System.Linq;
using ExpressionCalculator.Business.Entities;

namespace ExpressionCalculator.Business.DomainServices
{
    internal static class ExpressionService
    {
        public static Expression GetLeftExpressionByIndex(this Expression expression, int index)
        {
            var newExpression = new Expression();
            for (var i = 0; i < index; i++)
            {
                newExpression.Operators.Add(expression.Operators[i]);
                newExpression.Expressions.Add(expression.Expressions[i]);
            }
            
            newExpression.Expressions.Add(expression.Expressions[index]);

            if (newExpression.Expressions.Count == 1)
            {
                return newExpression.Expressions.FirstOrDefault();
            }

            return newExpression;
        }
        
        public static Expression GetRightExpressionByIndex(this Expression expression, int index)
        {
            var newExpression = new Expression();
            for (var i = index + 1; i < expression.Operators.Count; i++)
            {
                newExpression.Operators.Add(expression.Operators[i]);
                newExpression.Expressions.Add(expression.Expressions[i]);
            }
            
            newExpression.Expressions.Add(expression.Expressions.Last());

            if (newExpression.Expressions.Count == 1)
            {
                return newExpression.Expressions.FirstOrDefault();
            }
            
            return newExpression;
        }
    }
}