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

    public class HexMapRowColumnOffsetCoordinate : BaseTupleEquality<HexMapRowColumnOffsetCoordinate>
    {
        public HexMapRowColumnOffsetCoordinate(HexMapCubicCoordinate cubic)
        {
            var (parity, row, column) = CoordinateConverter.CubicQRToParityRowColumn(cubic.Q, cubic.R);

            Row = row;
            Column = column;
            IsShifted = parity % 2 != 0;
        }

        public static implicit operator HexMapRowColumnOffsetCoordinate((int, int) value) { return new HexMapRowColumnOffsetCoordinate(HexMapCubicCoordinate.FromRowColumn(value.Item1, value.Item2)); }
        public static implicit operator (int, int)(HexMapRowColumnOffsetCoordinate coordinate) { return (coordinate.Row, coordinate.Column); }

        public int Row { get; }
        public int Column { get; }
        public bool IsShifted { get; }

        public override string ToString()
        {
            if (IsShifted) return $"(R{Row},{Column}*)";
            return $"(R{Row},{Column})";
        }

        protected override (int, int) GetAsTuple()
        {
            return (Row, Column);
        }
    }

    public class HexMapCubicCoordinate : BaseTupleEquality<HexMapCubicCoordinate>
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

        public static implicit operator HexMapCubicCoordinate((int, int) value) { return new HexMapCubicCoordinate(value.Item1, value.Item2); }
        public static implicit operator (int, int)(HexMapCubicCoordinate coordinate) { return (coordinate.Q, coordinate.R); }

        public int Q { get; }
        public int R { get; }
        public int S => -Q - R;

        public override string ToString()
        {
            return $"[Q{Q},{R},{S}]";
        }

        protected override (int, int) GetAsTuple()
        {
            return (Q, R);
        }
    }

    public abstract class BaseTupleEquality<T>
    {
        protected abstract (int, int) GetAsTuple();

        #region Equality

        public override bool Equals(object? obj)
        {
            return Equals(obj as BaseTupleEquality<T>) || Equals(obj as (int, int)?);
        }

        public bool Equals((int, int)? other)
        {
            if (other == null) return false;

            var me = GetAsTuple();

            return
                me.Item1 == other.Value.Item1 &&
                me.Item2 == other.Value.Item2;
        }

        public bool Equals(BaseTupleEquality<T>? other)
        {
            if (other  == null) return false;

            var me = GetAsTuple();
            var you = other.GetAsTuple();

            return 
                me.Item1 == you.Item1 &&
                me.Item2 == you.Item2;
        }

        public override int GetHashCode()
        {
            var me = GetAsTuple();
            return HashCode.Combine(me.Item1, me.Item2);
        }

        public static bool operator ==((int, int)? left, BaseTupleEquality<T>? right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null) return false;

            var r = right.GetAsTuple();
            return 
                left.Value.Item1 == r.Item1 &&
                left.Value.Item2 == r.Item2;
        }

        public static bool operator ==(BaseTupleEquality<T>? left, BaseTupleEquality<T>? right)
        {
            return EqualityComparer<BaseTupleEquality<T>>.Default.Equals(left, right);
        }

        public static bool operator !=(BaseTupleEquality<T>? left, BaseTupleEquality<T>? right)
        {
            return !(left == right);
        }

        public static bool operator !=((int, int)? left, BaseTupleEquality<T>? right)
        {
            return !(left == right);
        }

        #endregion
    }
}
