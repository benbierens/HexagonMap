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
            return GetDistance(new DoNothingLog(), target);
        }

        public int GetDistance(ILog log, HexMapCoordinate target)
        {
            var dX = target.X - X;
            var dY = target.Y - Y;
            var absDx = Math.Abs(dX);
            var absDy = Math.Abs(dY);

            var gridwiseDistance = absDx + absDy;
            double smallestComponent = Math.Min(absDx, absDy);
            double largestComponent = Math.Max(absDx, absDy);
            var smallDiscount = Convert.ToInt32(Math.Min(smallestComponent, Math.Ceiling(smallestComponent / 2.0)));
            var largeDiscount = Convert.ToInt32(Math.Min(smallestComponent, Math.Ceiling(largestComponent / 2.0)));

            var selector = (dX > 0 && dY > 0) || (dX < 0 && dY < 0);
            var selectedDiscount = largeDiscount;

            log.Write($"distance calculation: {this} => {target}");
            log.Write($"gridwise: {gridwiseDistance} - smallest component: {smallestComponent}");
            log.Write($"smalldiscount: {smallDiscount}");
            log.Write($"largediscount: {largeDiscount}");
            log.Write($"select: {selector} -> discount: {selectedDiscount}");

            return gridwiseDistance - selectedDiscount;
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
