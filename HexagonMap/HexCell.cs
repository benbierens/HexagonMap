namespace HexagonMap
{
    public interface IHexCell : IHasNeighbors
    {
        HexMapCoordinate Coordinate { get; }
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
    }
}
