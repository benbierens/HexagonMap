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
    }
}
