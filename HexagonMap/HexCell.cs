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
        internal HexCell(IHexMap map, HexMapCoordinate coordinate)
        {
            Coordinate = coordinate;
            Neighbors = new HexCellNeighbor(map, coordinate);
        }

        public HexMapCoordinate Coordinate { get; }
        public IHexCellNeighbor Neighbors { get; }

        public int GetDistance(IHexCell target)
        {
            return Coordinate.GetDistance(target.Coordinate);
        }
    }
}
