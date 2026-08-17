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

    public class HexCell : IHexCell
    {
        public HexCell(HexMapCoordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public HexMapCoordinate Coordinate { get; }
    }
}
