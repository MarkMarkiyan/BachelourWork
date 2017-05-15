using System.Collections.Generic;
using GraphsExtensibility.Models;

namespace GraphSharpDemo.Service.Converters
{
    internal class StringToExpression
    {

        public IEnumerable<ExpressionElement> Convert(string s) {

            var expression = new List<ExpressionElement>();

            string number = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (IsNumber(s[i]))
                {
                    number += s[i];
                }
                else if (number.Length > 0)
                {
                    expression.Add(new ExpressionElement(number));
                    number = "";
                }
                else {
                    expression.Add(new ExpressionElement(s[i].ToString()));
                }
            }
            return expression;
        }

        private bool IsNumber(char s) {
            int result;
            return int.TryParse(s.ToString(), out result);
        }
    }
}