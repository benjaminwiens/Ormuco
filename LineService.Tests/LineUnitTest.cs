using System;
using Xunit;
using LineService;
using System.Collections.Generic;

namespace LineService.Tests
{
    public class LineUnitTest
    {
        [Theory]
        [MemberData(nameof(GetJointLines))]
        public void JointLines(Line first, Line second)
        {
            Assert.True(Line.DoLinesOverlap(first, second));
            Assert.True(Line.DoLinesOverlap(second, first)); //swap line order

            first.SwapCoordinates(); //only first line coordinates swapped
            Assert.True(Line.DoLinesOverlap(first, second));
            Assert.True(Line.DoLinesOverlap(second, first)); 

            second.SwapCoordinates(); //both lines coordinates swapped
            Assert.True(Line.DoLinesOverlap(first, second));
            Assert.True(Line.DoLinesOverlap(second, first)); 

            first.SwapCoordinates(); //only second line coordinates swapped
            Assert.True(Line.DoLinesOverlap(first, second));
            Assert.True(Line.DoLinesOverlap(second, first)); 

            second.SwapCoordinates();//reset back
            first.FlipOverYAxis(); //coordinates go positive -> negative and vice versa
            second.FlipOverYAxis();

            //repeat all
            Assert.True(Line.DoLinesOverlap(first, second));
            Assert.True(Line.DoLinesOverlap(second, first)); //swap line order

            first.SwapCoordinates(); //only first line coordinates swapped
            Assert.True(Line.DoLinesOverlap(first, second));
            Assert.True(Line.DoLinesOverlap(second, first)); 

            second.SwapCoordinates(); //both lines coordinates swapped
            Assert.True(Line.DoLinesOverlap(first, second));
            Assert.True(Line.DoLinesOverlap(second, first)); 

            first.SwapCoordinates(); //only second line coordinates swapped
            Assert.True(Line.DoLinesOverlap(first, second));
            Assert.True(Line.DoLinesOverlap(second, first)); 
        }

        [Theory]
        [MemberData(nameof(GetDisjointLines))]
        public void DisjointLines(Line first, Line second)
        {
            Assert.False(Line.DoLinesOverlap(first, second));
            Assert.False(Line.DoLinesOverlap(second, first)); //swap line order

            first.SwapCoordinates(); //only first line coordinates swapped
            Assert.False(Line.DoLinesOverlap(first, second));
            Assert.False(Line.DoLinesOverlap(second, first)); 

            second.SwapCoordinates(); //both lines coordinates swapped
            Assert.False(Line.DoLinesOverlap(first, second));
            Assert.False(Line.DoLinesOverlap(second, first)); 

            first.SwapCoordinates(); //only second line coordinates swapped
            Assert.False(Line.DoLinesOverlap(first, second));
            Assert.False(Line.DoLinesOverlap(second, first)); 

            second.SwapCoordinates();//reset back
            first.FlipOverYAxis(); //coordinates go positive -> negative and vice versa
            second.FlipOverYAxis();

            //repeat all
            Assert.False(Line.DoLinesOverlap(first, second));
            Assert.False(Line.DoLinesOverlap(second, first)); //swap line order

            first.SwapCoordinates(); //only first line coordinates swapped
            Assert.False(Line.DoLinesOverlap(first, second));
            Assert.False(Line.DoLinesOverlap(second, first)); 

            second.SwapCoordinates(); //both lines coordinates swapped
            Assert.False(Line.DoLinesOverlap(first, second));
            Assert.False(Line.DoLinesOverlap(second, first)); 

            first.SwapCoordinates(); //only second line coordinates swapped
            Assert.False(Line.DoLinesOverlap(first, second));
            Assert.False(Line.DoLinesOverlap(second, first)); 
        }

        public static IEnumerable<object[]> GetJointLines()
        {
            yield return new object[] //same line
            {
                new Line {_firstCoordinate = 1, _secondCoordinate = 2},
                new Line {_firstCoordinate = 1, _secondCoordinate = 2}
            };

            yield return new object[] //same first coordinnate
            {
                new Line {_firstCoordinate = 1, _secondCoordinate = 3},
                new Line {_firstCoordinate = 1, _secondCoordinate = 2}
            };

            yield return new object[] //overalap more than one point
            {
                new Line {_firstCoordinate = 1, _secondCoordinate = 3},
                new Line {_firstCoordinate = 2, _secondCoordinate = 4}
            };

            yield return new object[] //overlap only on one point 
            {
                new Line {_firstCoordinate = 7, _secondCoordinate = 9}, 
                new Line {_firstCoordinate = 13, _secondCoordinate = 9}
            };

            yield return new object[] //negative and positive
            {
                new Line {_firstCoordinate = -5, _secondCoordinate = -8}, 
                new Line {_firstCoordinate = 15, _secondCoordinate = -7}
            };

            yield return new object[] //zero overlap one point
            {
                new Line {_firstCoordinate = 0, _secondCoordinate = 3}, 
                new Line {_firstCoordinate = -1, _secondCoordinate = 0}
            };

            yield return new object[] //zero overlap more than one point
            {
                new Line {_firstCoordinate = 0, _secondCoordinate = 3}, 
                new Line {_firstCoordinate = -1, _secondCoordinate = 2}
            };
        }

        public static IEnumerable<object[]> GetDisjointLines()
        {
            yield return new object[] //different lines
            {
                new Line {_firstCoordinate = 1, _secondCoordinate = 2},
                new Line {_firstCoordinate = 3, _secondCoordinate = 5}
            };

            yield return new object[] //zero 
            {
                new Line {_firstCoordinate = 0, _secondCoordinate = 5},
                new Line {_firstCoordinate = 9, _secondCoordinate = 10}
            };

            yield return new object[] //positive and negative
            {
                new Line {_firstCoordinate = 4, _secondCoordinate = 3},
                new Line {_firstCoordinate = -7, _secondCoordinate = -10}
            };
        }
    }
}