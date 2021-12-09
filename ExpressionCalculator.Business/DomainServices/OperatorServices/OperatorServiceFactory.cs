using System;
using System.Collections.Generic;
using ExpressionCalculator.Business.ValueObjects.Operators;

namespace ExpressionCalculator.Business.DomainServices.OperatorServices
{
    internal static class OperatorServiceFactory
    {
        private static readonly IReadOnlyDictionary<Type, OperatorService> operators = new Dictionary<Type, OperatorService>
        {
            {typeof(AdditionOperator), new AdditionOperatorServiceService()},
            {typeof(SubtractOperator), new SubtractOperatorService()},
            {typeof(MultiplicationOperator), new MultiplicationOperatorServiceService()},
            {typeof(DivisionOperator), new DivisionOperatorServiceService()},
        };

        public static OperatorService GetOperatorService(Operator op)
        {
            return operators[op.GetType()];
        }
    }
}