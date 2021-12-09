using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ExpressionCalculator.Business.DomainServices;
using ExpressionCalculator.Business.DomainServices.OperatorServices;
using ExpressionCalculator.Business.Entities;
using ExpressionCalculator.Business.ValueObjects;
using ExpressionCalculator.Business.ValueObjects.Errors;
using ExpressionCalculator.Business.ValueObjects.Operators;

[assembly: InternalsVisibleTo("ExpressionCalculator.Business.Tests")]
namespace ExpressionCalculator.Business.ApplicationService
{
    internal sealed class ExpressionFiniteAutomate : IExpressionFiniteAutomate
    {
        private readonly string allowedCharaters = "0123456789+-/*({[)}]";
        private readonly IReadOnlyDictionary<string, ExpressionStates> stateOfTheCharacter =
            new Dictionary<string, ExpressionStates>
            {
                {"0123456789", ExpressionStates.Number},
                {"+-/*", ExpressionStates.Operator},
                {"({[", ExpressionStates.OpenBracket},
                {")}]", ExpressionStates.CloseBracket},
            };

        private readonly IReadOnlyDictionary<char, char> relatedCloseBracket = new Dictionary<char, char>
        {
            {'(', ')'},
            {'[', ']'},
            {'{', '}'}
        };

        private readonly IReadOnlyDictionary<ExpressionStates, IReadOnlyCollection<ExpressionStates>>
            acceptedNextStates =
                new Dictionary<ExpressionStates, IReadOnlyCollection<ExpressionStates>>
                {
                    {
                        ExpressionStates.Start,
                        new List<ExpressionStates> {ExpressionStates.Number, ExpressionStates.OpenBracket}
                    },
                    {
                        ExpressionStates.Number,
                        new List<ExpressionStates>
                            {ExpressionStates.Operator, ExpressionStates.CloseBracket, ExpressionStates.End}
                    },
                    {
                        ExpressionStates.Operator,
                        new List<ExpressionStates> {ExpressionStates.Number, ExpressionStates.OpenBracket}
                    },
                    {
                        ExpressionStates.OpenBracket,
                        new List<ExpressionStates> {ExpressionStates.Number, ExpressionStates.OpenBracket}
                    },
                    {
                        ExpressionStates.CloseBracket,
                        new List<ExpressionStates>
                            {ExpressionStates.Operator, ExpressionStates.CloseBracket, ExpressionStates.End}
                    },
                    {
                        ExpressionStates.End,
                        new List<ExpressionStates>
                            {ExpressionStates.End}
                    }
                };

        public Node CreateExpressionTree(string input)
        {
            var nextPossibleStates = acceptedNextStates[ExpressionStates.Start];
            var brackets = new Stack<char>();
            var errors = new List<Error>();

            var expression = ParseExpression(input, 0, nextPossibleStates, brackets, errors);
            if (errors.Count != 0)
            {
                throw new Exception(errors[0].GetErrorMessage());
            }

            return CreateExpressionTree(expression);
        }

        private Node CreateExpressionTree(Expression expression)
        {
            if (expression.IsOperand())
            {
                return new OperandNode(Int32.Parse(expression.Tokens.FirstOrDefault()?.Value));
            }

            if (expression.Expressions.Count == 1)
            {
                return CreateExpressionTree(expression.Expressions.FirstOrDefault());
            }
            
            var index = expression.Operators.GetDominantOperatorIndex();
            var left = expression.GetLeftExpressionByIndex(index);
            var right = expression.GetRightExpressionByIndex(index);

            var node = new OperatorNode(expression.Operators[index]);
            node.Left = CreateExpressionTree(left);
            node.Right = CreateExpressionTree(right);

            return node;
        }
        
        private Expression ParseExpression(string input, int index, IReadOnlyCollection<ExpressionStates> nextPossibleStates, Stack<char> brackets, ICollection<Error> errors)
        {
            if (errors.Count != 0)
            {
                return null;
            }
            
            var expression = new Expression();
            while (true)
            {
                var currentState = GetStateByCharacter(input, index);
                if (currentState == ExpressionStates.End)
                {
                    return expression;
                }
                
                var currentCharacter = input[index];
                var stateError = GetStateError(currentState, index, currentCharacter, nextPossibleStates, brackets);
                if (stateError != null)
                {
                    errors.Add(stateError);
                    return null;
                }

                nextPossibleStates = acceptedNextStates[currentState];
                
                if (currentState == ExpressionStates.OpenBracket)
                {
                    brackets.Push(currentCharacter);
                    var openBracketToken = GetToken(input, index, currentState);
                    expression.Tokens.Add(openBracketToken);

                    var insideExpression = ParseExpression(input, index + 1, nextPossibleStates, brackets, errors);
                    if (insideExpression == null)
                    {
                        return null;
                    }
                    
                    expression.Expressions.Add(insideExpression);
                    index = insideExpression.GetLastIndex() + 1;
                    nextPossibleStates = acceptedNextStates[ExpressionStates.CloseBracket];
                    continue;
                }

                if (currentState == ExpressionStates.CloseBracket)
                {
                    brackets.Pop();
                    
                    var closeBracketToken = GetToken(input, index, currentState);
                    expression.Tokens.Add(closeBracketToken);
                    
                    return expression;
                }
                
                var token = GetToken(input, index, currentState);
                expression.Tokens.Add(token);

                if (currentState == ExpressionStates.Operator)
                {
                    expression.Operators.Add(OperatorFactory.GetOperator(currentCharacter, index));
                }

                if (currentState == ExpressionStates.Number)
                {
                    expression.Expressions.Add(Expression.CreateOperandExpression(token));
                }
                
                index = token.EndIndex + 1;
            }
        }

        private Error GetStateError(ExpressionStates currentState, int currentIndex, char currentCharacter, IReadOnlyCollection<ExpressionStates> nextPossibleStates, Stack<char> brackets)
        {
            if (!allowedCharaters.Contains(currentCharacter))
            {
                return new ExpressionIllegalCharacterError(currentIndex, currentCharacter);
            }
            
            if (!nextPossibleStates.Contains(currentState))
            {
                return new ExpressionInvalidStateError(currentIndex, currentState, nextPossibleStates);
            }

            if (currentState == ExpressionStates.CloseBracket 
                && (brackets.Count == 0 || !IsTheCorrectBracket(brackets.Peek(), currentCharacter)))
            {
                return new ExpressionWrongCloseBracketError(currentIndex, currentCharacter,
                    brackets.Count == 0 ? ' ': relatedCloseBracket[brackets.Peek()]);
            }

            return null;
        }

        private bool IsTheCorrectBracket(char lastOpenBracket, char closeBracket)
        {
            return !((closeBracket == ')' && lastOpenBracket != '(')
                     || (closeBracket == ']' && lastOpenBracket != '[')
                     || (closeBracket == '}' && lastOpenBracket != '{'));
        }

        private ExpressionStates GetStateByCharacter(string input, int index)
        {
            if (index == input.Length)
            {
                return ExpressionStates.End;
            }
            
            return stateOfTheCharacter.FirstOrDefault(x => x.Key.Contains(input[index])).Value;
        }

        private Token GetToken(string input, int index, ExpressionStates state)
        {
            var endIndex = index;
            if (state == ExpressionStates.Number && index < input.Length - 1)
            {
                while (char.IsDigit(input[endIndex + 1])) endIndex++;
            }

            return new Token(input.Substring(index, endIndex - index + 1), index, endIndex);
        }
    }
}