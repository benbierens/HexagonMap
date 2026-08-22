namespace HexagonMap
{
    public class HexMapCoordinate : IEquatable<HexMapCoordinate?>
    {
        private HexMapCubicCoordinate? cubicCoordinate = null;

        public HexMapCoordinate(int x, int y)
        {
            X = x;
            Y = y;
            IsShifted = y % 2 != 0;
        }

        public int X { get; }
        public int Y { get; }
        internal bool IsShifted { get; }

        public HexMapCubicCoordinate AsCubic
        {
            get
            {
                if (cubicCoordinate == null) cubicCoordinate = new HexMapCubicCoordinate(this);
                return cubicCoordinate;
            }
        }

        public int GetDistance(HexMapCoordinate target)
        {
            return GetDistance(new DoNothingLog(), target);
        }

        public int GetDistance(ILog log, HexMapCoordinate target)
        {
            var thisCube = AsCubic;
            var targetCube = target.AsCubic;
            
            var dx = Math.Abs(thisCube.X - targetCube.X);
            var dy = Math.Abs(thisCube.Y - targetCube.Y);
            var dz = Math.Abs(thisCube.Z - targetCube.Z);

            var distance = (dx + dy + dz) / 2;
            log.Write($"thisCube:{thisCube} - targetCube: {targetCube} - distance = {distance}");

            return distance;
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

    public class HexMapCubicCoordinate
    {
        public HexMapCubicCoordinate(HexMapCoordinate coordinate)
        {
            var axialQ = coordinate.X - ((coordinate.Y - (coordinate.Y & 1)) / 2);

            X = axialQ;
            Z = coordinate.Y;
            Y = -X - Z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public override string ToString()
        {
            return $"[{X},{Y},{Z}]";
        }
    }
}
