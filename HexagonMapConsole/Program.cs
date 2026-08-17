namespace HexagonMap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var map = new IntMap();

            var cell = map.GetCell(new IntCoordinate(0, 1));

            Console.WriteLine($"Cell: {cell.HexCell.Position.Coordinate.X},{cell.HexCell.Position.Coordinate.Y}");
        }
    }

    public class IntCoordinate : Coordinate<int>
    {
        public IntCoordinate(int x, int y)
            : base(x, y)
        {
        }
    }

    public class IntCell : IHasHexCell<int>
    {
        public IntCell(IHexCell<int> hexCell)
        {
            HexCell = hexCell;
        }

        public IHexCell<int> HexCell { get; }
    }

    public class IntPersistence : InMemoryHexMapPersistence<int, IntCell>
    {
    }

    public class IntMath : IMath<int>
    {
        public bool IsEven(int value)
        {
            return value % 2 == 0;
        }
    }

    public class IntCellFactory : IUserCellFactory<int, IntCell>
    {
        public IntCell CreateCell(IHexCell<int> hexCell)
        {
            return new IntCell(hexCell);
        }
    }

    public class IntMap : HexMap<int, IntCell>
    {
        public IntMap()
            : base(
                  new IntMath(),
                  new IntPersistence(),
                  new IntCellFactory(),
                  new DoNothingCoordinateTransformer<int>()
            )
        {
        }
    }
}
