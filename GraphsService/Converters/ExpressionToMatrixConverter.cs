using GraphsExtensibility.Models;
using GraphsExtensibility.Models.Enums;
using System.Collections.Generic;

namespace GraphsService.Converters
{
    public class ExpressionToMatrixConverter
    {
        public string[,] Convert(List<ExpressionElement> expression)
        {
            var argumentsCount = GetNumberOfArgumants(expression);
            var operationsCount = GetNumberOfOperations(expression);
            var elementsCount = argumentsCount + operationsCount;

            var matrix = new string[operationsCount, argumentsCount + operationsCount];
            var elemsPositions = new List<int?>();

            int numCount = 0;

            for (int i = 0; i < elementsCount; i++)
            {
                    elemsPositions.Add(null);
            }

            for (int i = 0; i < elementsCount; i++)
            {
                if (expression[i].Type == ElementType.Number)
                {
                    elemsPositions[i] = numCount;
                    numCount++;
                }
            }

            int? firstParam = null;
            int? secondParam = null;

            for (int i = 0, argPos  = 0; i < elementsCount; i++)
            {
                if (expression[i].Type == ElementType.Number)
                {
                    if (firstParam == null)
                        firstParam = argPos;
                    else
                        secondParam = argPos;
                    argPos++;
                }  
                if (firstParam != null && secondParam != null)
                {
                    matrix[(int)firstParam, numCount] = "1";
                    matrix[(int)secondParam, numCount] = "1";
                    
                    numCount++;
                     elemsPositions[i - 1] = numCount;
                    firstParam = null;
                    secondParam = null;
                }
            }
            return matrix;
        }

        private int GetNumberOfArgumants(IEnumerable<ExpressionElement> expession)
        {
            int count = 0;
            foreach (var item in expession)
            {
                if (item.Type == ElementType.Number)
                {
                    count++;
                }
            }
            return count;
        }

        private int GetNumberOfOperations(IEnumerable<ExpressionElement> expession)
        {
            int count = 0;
            foreach (var item in expession)
            {
                if (item.Type != ElementType.Number)
                {
                    count++;
                }
            }
            return count;
        }
    }
}