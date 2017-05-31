using System.Collections.Generic;
using GraphsExtensibility.Models;
using System.Text;
using GraphsExtensibility.Models.Enums;

namespace GraphSharpDemo.Service.Converters
{
    public class StringToExpressionConverter
    {
        private Dictionary<string, ElementType> elementTypesNames;

        public StringToExpressionConverter() {
            elementTypesNames = new Dictionary<string, ElementType> {
                { "+", ElementType.Plus},
                { "-", ElementType.Minus},
                { "*", ElementType.Divide},
                {"/", ElementType.Product }
            };
        }

        public IEnumerable<ExpressionElement> Convert(string s)
        {
            var expression = new List<ExpressionElement>();

            string number = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                if (IsNumber(s[i]))
                {
                    number += s[i];
                }
                else
                {
                    if (number.Length > 0)
                    {
                        expression.Add(
                            new ExpressionElement
                            {
                                Type = ElementType.Number,
                                Value = int.Parse(number)
                            });
                        number = string.Empty;
                    }
                    expression.Add(new ExpressionElement
                    {
                        Type = elementTypesNames[s[i].ToString()],
                        Value = null
                    });
                   
                }
            }
            if (number.Length > 0)
            {
                expression.Add(
                    new ExpressionElement
                    {
                        Type = ElementType.Number,
                        Value = int.Parse(number)
                    });
            }
            return expression;
        }

        private bool IsNumber(char s)
        {
            int result;
            return int.TryParse(s.ToString(), out result);
        }

        private string GetStringWithoutWhiteSpaces(string str)
        {
            StringBuilder newString = new StringBuilder("");

            foreach (var s in str)
            {
                if (s != ' ')
                {
                    newString.Append(s);
                }
            }
            return newString.ToString();
        }
    }
}