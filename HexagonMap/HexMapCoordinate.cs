namespace HexagonMap
{
    public static class CoordinateConverter
    {
        public static (int, int, int) CubicQRToParityRowColumn(int q, int r)
        {
            var row = r;
            var parity = row & 1;
            var column = q + (r - parity) / 2;
            return (parity, row, column);
        }

        public static (int, int) RowColumnToCubicQR(int row, int column)
        {
            var parity = row & 1;
            var axialQ = column - ((row - parity) / 2);
            return (axialQ, row);
        }
    }

    public class HexMapRowColumnOffsetCoordinate
    {
        public HexMapRowColumnOffsetCoordinate(HexMapCubicCoordinate cubic)
        {
            var (parity, row, column) = CoordinateConverter.CubicQRToParityRowColumn(cubic.Q, cubic.R);

            Row = row;
            Column = column;
            IsShifted = parity % 2 != 0;
        }

        public int Row { get; }
        public int Column { get; }
        public bool IsShifted { get; }

        public override string ToString()
        {
            if (IsShifted) return $"(R{Row},{Column}*)";
            return $"(R{Row},{Column})";
        }
    }

    public class HexMapCubicCoordinate
    {
        public static HexMapCubicCoordinate FromRowColumn(int row, int column)
        {
            var (q, r) = CoordinateConverter.RowColumnToCubicQR(row, column);
            return new HexMapCubicCoordinate(q, r);
        }

        public HexMapCubicCoordinate(int q, int r)
        {
            Q = q;
            R = r;
        }

        public int Q { get; }
        public int R { get; }
        public int S => -Q - R;

        public override string ToString()
        {
            return $"[Q{Q},{R},{S}]";
        }
    }
}
