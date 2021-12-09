using System.Collections.Generic;
using System.Linq;
using ExpressionCalculator.Business.ValueObjects.Operators;

namespace ExpressionCalculator.Business.DomainServices.OperatorServices
{
    internal static class OperatorsService
    {
        public static int GetDominantOperatorIndex(this IList<Operator> operators)
        {
            var maxDominantOperator = operators.Max(x => x.GetDominantIndex());
            var dominantOperator = operators.LastOrDefault(x => x.GetDominantIndex() == maxDominantOperator);
            
            return operators.IndexOf(dominantOperator);
        }
    }
}