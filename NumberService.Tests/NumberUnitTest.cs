using System;
using Xunit;
using NumberService;
using System.Collections.Generic;

namespace NumberService.Tests
{
    public class NumberUnitTest
    {
        [Theory]
        [InlineData("1.2","1.2")]
        [InlineData("  1.2 "," 1.2")]
        [InlineData("0","0")]
        [InlineData("-1.5","-1.5")]
        [InlineData("1003.52","1003.52")]
        public void Equal(string num1, string num2)
        {
            Assert.Equal(Number.CompareNumbers(num1,num2), string.Format("{0} is {1} {2}", num1, Number._EQUAL, num2));  
        }

        [Theory] //Inline data has num1 > num2 since it also tests num2 < num1
        [InlineData("1.3","1.29")]
        [InlineData("-1","-1.5")]
        [InlineData("0","-5")]
        [InlineData("5000.54","2000")]
        public void InEquality(string num1, string num2)
        {
            Assert.Equal(Number.CompareNumbers(num1,num2), string.Format("{0} is {1} {2}", num1, Number._GREATER, num2));
            Assert.Equal(Number.CompareNumbers(num2,num1), string.Format("{0} is {1} {2}", num2, Number._LESS, num1));  
        }

        [Theory]
        [InlineData("a1.3","1.29")]
        [InlineData("-1.5","asd")]
        [InlineData("1 0","12")]
        [InlineData("","")]
        [InlineData(null,null)]
        [InlineData(" "," ")]
        public void Error(string num1, string num2)
        {
            Assert.Equal(Number.CompareNumbers(num1,num2), Number._ERROR);  
        }
    }
}