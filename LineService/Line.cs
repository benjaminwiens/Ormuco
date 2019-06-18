using System;

namespace LineService
{
    public class Line
    {
        public int _firstCoordinate { get; set; }
        public int _secondCoordinate { get; set; }

        public static bool DoLinesOverlap(Line lineOne, Line lineTwo) //a dot is not considered a line, assume no dots are given
        {
            //order the coordinates of each line (min is far leftmost)
            int lineOneLeftCoordinate = Math.Min(lineOne._firstCoordinate, lineOne._secondCoordinate);
            int lineOneRightCoordinate = Math.Max(lineOne._firstCoordinate, lineOne._secondCoordinate);

            int lineTwoLeftCoordinate = Math.Min(lineTwo._firstCoordinate, lineTwo._secondCoordinate);
            int lineTwoRightCoordinate = Math.Max(lineTwo._firstCoordinate, lineTwo._secondCoordinate);

            return lineOneLeftCoordinate < lineTwoLeftCoordinate ? //check which line is left most
               !(lineOneRightCoordinate < lineTwoLeftCoordinate) :
               !(lineTwoRightCoordinate < lineOneLeftCoordinate);
        }

        public void FlipOverYAxis()
        {
            _firstCoordinate *= -1;
            _secondCoordinate *= -1;
        }
        public void SwapCoordinates()
        {
            int temp = _firstCoordinate;
            _firstCoordinate = _secondCoordinate;
            _secondCoordinate = temp;
        }
    }
}