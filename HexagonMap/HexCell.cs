namespace HexagonMap
{
    public interface IHexCell
    {
        HexMapCoordinate Coordinate { get; }
    }

    public interface IHasHexCell
    {
        IHexCell HexCell { get; }
    }

    internal class HexCell : IHexCell
    {
        internal HexCell(HexMapCoordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public HexMapCoordinate Coordinate { get; }
    }
}
