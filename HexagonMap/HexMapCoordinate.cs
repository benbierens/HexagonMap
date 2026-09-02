namespace HexagonMap
{
    public class HexMapCoordinate : IEquatable<HexMapCoordinate?>
    {
        public HexMapCoordinate(HexMapCubicCoordinate cubic)
        {
            AsCubic = cubic;
            AsRowColumn = new HexMapRowColumnOffsetCoordinate(cubic);
        }

        public static HexMapCoordinate FromRowColumn(int row, int column)
        {
            var parity = row & 1;
            var axialQ = column - ((row - parity) / 2);
            return FromCubic(
                q: axialQ,
                r: row
            );
        }

        public static HexMapCoordinate FromCubic(int q, int r)
        {
            return new HexMapCoordinate(new HexMapCubicCoordinate(
                q: q,
                r: r
            ));
        }

        public HexMapRowColumnOffsetCoordinate AsRowColumn { get; }
        public HexMapCubicCoordinate AsCubic { get;}

        public int GetDistance(HexMapCoordinate target)
        {
            return GetDistance(new DoNothingLog(), target);
        }

        public int GetDistance(ILog log, HexMapCoordinate target)
        {
            var thisCube = AsCubic;
            var targetCube = target.AsCubic;
            
            var dx = Math.Abs(thisCube.Q - targetCube.Q);
            var dy = Math.Abs(thisCube.R - targetCube.R);
            var dz = Math.Abs(thisCube.S - targetCube.S);

            var distance = (dx + dy + dz) / 2;
            log.Write($"thisCube:{thisCube} - targetCube: {targetCube} - distance = {distance}");

            return distance;
        }

        public override string ToString()
        {
            return AsRowColumn.ToString() + AsCubic.ToString();
        }

        public static implicit operator HexMapCoordinate((int, int) value) { return HexMapCoordinate.FromRowColumn(value.Item1, value.Item2); }
        public static implicit operator (int, int)(HexMapCoordinate coordinate) { return (coordinate.AsRowColumn.Column, coordinate.AsRowColumn.Row); }

        #region Equality

        public override bool Equals(object? obj)
        {
            return Equals(obj as HexMapCoordinate) || Equals(obj as (int, int)?);
        }

        public bool Equals(HexMapCoordinate? other)
        {
            return other is not null &&
                   AsCubic.Q == other.AsCubic.Q &&
                   AsCubic.R == other.AsCubic.R &&
                   AsCubic.S == other.AsCubic.S;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AsCubic.Q, AsCubic.R, AsCubic.S);
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

    public class HexMapRowColumnOffsetCoordinate
    {
        public HexMapRowColumnOffsetCoordinate(HexMapCubicCoordinate cubic)
        {
            var parity = Row & 1;

            Row = cubic.R;
            Column = cubic.Q + (Row - parity) / 2;
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
