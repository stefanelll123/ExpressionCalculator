using System.Collections.Generic;
using System.Linq;
using ExpressionCalculator.Business.ValueObjects;
using ExpressionCalculator.Business.ValueObjects.Operators;

namespace ExpressionCalculator.Business.Entities
{
    internal sealed class Expression
    {
        public readonly IList<Token> Tokens = new List<Token>();
        public readonly IList<Operator> Operators = new List<Operator>();
        public readonly IList<Expression> Expressions = new List<Expression>();

        public bool IsOperand()
        {
            return Tokens.Count == 1 && Expressions.Count == 0 && Operators.Count == 0;
        }

        public bool IsEmptyExpression()
        {
            return Tokens.Count == 0;
        }

        public int GetLastIndex()
        {
            return Tokens.Last().EndIndex;
        }

        public static Expression CreateOperandExpression(Token token)
        {
            var expression = new Expression();
            expression.Tokens.Add(token);

            return expression;
        }
    }
}