namespace HexagonMap
{
    public class HexMapCoordinate : IEquatable<HexMapCoordinate?>
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
            var dX = Math.Abs(target.X - X);
            var dY= Math.Abs(target.Y - Y);

            var gridwiseDistance = dX + dY;
            double smallestComponent = Math.Min(dX, dX);
            var diagonalsDiscount = Math.Min(dY, Convert.ToInt32(Math.Ceiling(smallestComponent / 2.0)));

            return gridwiseDistance - diagonalsDiscount;
        }

        public override string ToString()
        {
            if (IsShifted) return $"({X},{Y}*)";
            return $"({X},{Y})";
        }

        public static implicit operator HexMapCoordinate((int, int) value) { return new HexMapCoordinate(value.Item1, value.Item2); }
        public static implicit operator (int, int)(HexMapCoordinate coordinate) { return (coordinate.X, coordinate.Y); }

        #region Equality

        public override bool Equals(object? obj)
        {
            return Equals(obj as HexMapCoordinate) || Equals(obj as (int, int)?);
        }

        public bool Equals(HexMapCoordinate? other)
        {
            return other is not null &&
                   X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==((int, int)? left, HexMapCoordinate? right)
        {
            HexMapCoordinate? l = left;
            return l == right;
        }

        public static bool operator ==(HexMapCoordinate? left, HexMapCoordinate? right)
        {
            return EqualityComparer<HexMapCoordinate>.Default.Equals(left, right);
        }

        public static bool operator !=(HexMapCoordinate? left, HexMapCoordinate? right)
        {
            return !(left == right);
        }

        public static bool operator !=((int, int)? left, HexMapCoordinate? right)
        {
            return !(left == right);
        }

        #endregion
    }
}
