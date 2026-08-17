namespace HexagonMap
{
    public interface IHasNeighbors
    {
        IHexCellNeighbor Neighbors { get; }
    }

    public interface IHexCellNeighbor
    {
        IHexCellNeighbor this[Direction direction] { get; }
        IHexCell GetCell();
    }

    internal class HexCellNeighbor : IHexCellNeighbor
    {
        private readonly HexMapCoordinate coordinate;
        private readonly IHexMap map;
        private IHexCellNeighbor[] neighbors = null!;

        public HexCellNeighbor(IHexMap map, HexMapCoordinate coordinate)
        {
            this.coordinate = coordinate;
            this.map = map;
        }

        public IHexCellNeighbor this[Direction direction]
        {
            get
            {
                if (neighbors == null) neighbors = PopulateNeighbors();
                return neighbors[direction];
            }
        }

        public IHexCell GetCell()
        {
            return map.GetCell(coordinate);
        }

        private IHexCellNeighbor[] PopulateNeighbors()
        {
            var c = coordinate;
            if (c.IsShifted)
            {
                return
                [
                    new HexCellNeighbor(map, new HexMapCoordinate(c.X, c.Y - 1)),
                    new HexCellNeighbor(map, new HexMapCoordinate(c.X + 1, c.Y - 1)),
                    new HexCellNeighbor(map, new HexMapCoordinate(c.X + 1, c.Y)),
                    new HexCellNeighbor(map, new HexMapCoordinate(c.X + 1, c.Y + 1)),
                    new HexCellNeighbor(map, new HexMapCoordinate(c.X, c.Y + 1)),
                    new HexCellNeighbor(map, new HexMapCoordinate(c.X - 1, c.Y))
                ];
            }

            return
            [
                new HexCellNeighbor(map, new HexMapCoordinate(c.X - 1, c.Y - 1)),
                new HexCellNeighbor(map, new HexMapCoordinate(c.X, c.Y - 1)),
                new HexCellNeighbor(map, new HexMapCoordinate(c.X + 1, c.Y)),
                new HexCellNeighbor(map, new HexMapCoordinate(c.X, c.Y + 1)),
                new HexCellNeighbor(map, new HexMapCoordinate(c.X - 1, c.Y + 1)),
                new HexCellNeighbor(map, new HexMapCoordinate(c.X - 1, c.Y))
            ];
        }
    }
}
