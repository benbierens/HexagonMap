namespace HexagonMap
{
    public interface IHexCell : IEquatable<IHexCell?>
    {
        HexMapRowColumnOffsetCoordinate AsRowColumn { get; }
        HexMapCubicCoordinate AsCubic { get; }

        int GetDistance(IHexCell target);
        IHexCell GetNeighbor(Direction direction);
    }

    internal class HexCell : IHexCell
    {
        private readonly ILog log;
        private readonly HexMap map;

        internal HexCell(ILog log, HexMap map, HexMapCubicCoordinate cubic)
        {
            this.log = log;
            this.map = map;

            AsCubic = cubic;
            AsRowColumn = new HexMapRowColumnOffsetCoordinate(cubic);

            log.Write("Created: " + ToString());
        }

        internal static IHexCell FromRowColumn(ILog log, HexMap map, int row, int column)
        {
            var (q, r) = CoordinateConverter.RowColumnToCubicQR(row, column);
            return FromCubic(log, map,
                q: q,
                r: r
            );
        }

        internal static IHexCell FromCubic(ILog log, HexMap map, int q, int r)
        {
            return new HexCell(log, map, new HexMapCubicCoordinate(
                q: q,
                r: r
            ));
        }

        public HexMapRowColumnOffsetCoordinate AsRowColumn { get; }
        public HexMapCubicCoordinate AsCubic { get; }

        public int GetDistance(IHexCell target)
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

        public IHexCell GetNeighbor(Direction direction)
        {
            return map.GetCell(direction.TransformCubic(AsCubic));
        }

        #region Equality

        public override bool Equals(object? obj)
        {
            return Equals(obj as IHexCell) || Equals(obj as (int, int)?);
        }

        public bool Equals(IHexCell? other)
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

        public static bool operator ==(HexCell? left, IHexCell? right)
        {
            return EqualityComparer<IHexCell>.Default.Equals(left, right);
        }

        public static bool operator !=(HexCell? left, IHexCell? right)
        {
            return !(left == right);
        }

        #endregion

        public override string ToString()
        {
            return AsRowColumn.ToString() + AsCubic.ToString();
        }
    }
}
