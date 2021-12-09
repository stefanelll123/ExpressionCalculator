using ExpressionCalculator.Business.Entities;

namespace ExpressionCalculator.Business.ApplicationService
{
    public interface IExpressionFiniteAutomate
    {
        Node CreateExpressionTree(string input);
    }
}