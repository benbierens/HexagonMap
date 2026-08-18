using System.Diagnostics;

namespace HexagonMap
{
    public class HexMapCoordinate
    {
        public HexMapCoordinate(int x, int y)
        {
            X = x;
            Y = y;
            IsShifted = y % 2 != 0;
        }

        public int X { get; }
        public int Y { get; }
        internal bool IsShifted { get; }

        public int GetDistance(HexMapCoordinate target)
        {
            var xcontrib = Math.Abs(target.X - X);
            var ycontrib = Math.Abs(target.Y - Y);
            Debug.WriteLine("xc: " + xcontrib);
            Debug.WriteLine("yc: " + ycontrib);
            return xcontrib + ycontrib;
        }
    }
}
