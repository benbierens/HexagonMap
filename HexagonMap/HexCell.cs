namespace HexagonMap
{
    public interface IHexCell : IHasNeighbors
    {
        HexMapCoordinate Coordinate { get; }
        int GetDistance(IHexCell target);
    }

    public interface IHasHexCell
    {
        IHexCell HexCell { get; }
    }

    internal class HexCell : IHexCell
    {
        private readonly ILog log;

        internal HexCell(ILog log, IHexMap map, HexMapCoordinate coordinate)
        {
            this.log = log;
            Coordinate = coordinate;
            Neighbors = new HexCellNeighbor(map, coordinate);
        }

        public HexMapCoordinate Coordinate { get; }
        public IHexCellNeighbor Neighbors { get; }

        public int GetDistance(IHexCell target)
        {
            return Coordinate.GetDistance(log, target.Coordinate);
        }

        public override string ToString()
        {
            return Coordinate.ToString();
        }
    }
}
