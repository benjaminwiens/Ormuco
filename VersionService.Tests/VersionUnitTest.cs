using System;
using Xunit;
using VersionService;

namespace VersionService.Tests
{
    public class VersionUnitTest
    {
        [Theory]
        [InlineData("1.2","1.2")]
        [InlineData("  1.2 "," 1.2")]
        [InlineData("0","0")]
        [InlineData("1003.52","1003.52")]
        [InlineData("1003.1.213.52","1003.1.213.52")]
        public void Equal(string ver1, string ver2)
        {
            Assert.Equal($"{ver1} is equal to {ver2}", Version.CompareVersions(ver1,ver2));
        }

        [Theory] //Inline data has num1 > num2 since it also tests num2 < num1
        [InlineData("1.29","1.3")]
        [InlineData("1.17.1.2","1.17.1.1")]
        [InlineData(" 0.0.0.1","0.0.0.0 ")]
        [InlineData("100.415.2","100.414.3")]
        public void InEquality(string ver1, string ver2)
        {
            Assert.Equal($"{ver1} is greater than {ver2}", Version.CompareVersions(ver1,ver2));
            Assert.Equal($"{ver2} is less than {ver1}", Version.CompareVersions(ver2,ver1));
        }

        [Theory]
        [InlineData("a1.3","1.29")]
        [InlineData("-1.5","1.5")]
        [InlineData("1..0","1.0")]
        [InlineData("","")]
        [InlineData(null,null)]
        [InlineData("1.5.2","1.5.2.0")]
        public void Error(string ver1, string ver2)
        {
            Assert.Equal("Version strings must be valid and of same length", Version.CompareVersions(ver1,ver2));  
        }
    }
}