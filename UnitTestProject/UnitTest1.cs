using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphSharpDemo.Service.Converters;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {

        private StringToExpressionConverter converter;
         
        [TestMethod]
        public void TestMethod1()
        {
            converter = new StringToExpressionConverter();

            converter.Convert("a+b*c+d");
        }
    }
}
