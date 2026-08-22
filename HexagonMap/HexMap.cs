namespace HexagonMap
{
    public interface IHexMap
    {
        IHexCell GetCell(int x, int y);
        IHexCell GetCell(HexMapCoordinate coordinate);
    }

    public class HexMap : IHexMap
    {
        private readonly ILog log;
        private readonly IHexMapPersistence persistence;

        public HexMap(ILog log, IHexMapPersistence persistence)
        {
            this.log = log;
            this.persistence = persistence;
        }

        public IHexCell GetCell(int x, int y)
        {
            return GetCell(new HexMapCoordinate(x, y));
        }

        public IHexCell GetCell(HexMapCoordinate c)
        {
            var cell = persistence.Read(c);
            if (cell != null) return cell;

            cell = new HexCell(log, this, c);
            persistence.Write(cell);
            return cell;
        }

        public void DeleteCell(int x, int y)
        {
            DeleteCell(new HexMapCoordinate(x, y));
        }

        public void DeleteCell(HexMapCoordinate coordinate)
        {
            persistence.Delete(coordinate);
        }

        public void Print()
        {
            var coords = new List<HexMapCoordinate>();

            ((InMemoryHexMapPersistence)persistence).Iterate(cell =>
            {
                coords.Add(cell.Coordinate);
            });

            var xMin = coords.Min(c => c.X);
            var xMax = coords.Max(c => c.X);
            var yMin = coords.Min(c => c.Y);
            var yMax = coords.Max(c => c.Y);

            log.Write(" - ");
            for (var y = yMin; y <= yMax; y++)
            {
                var line = "";
                if (y % 2 != 0) line += "    ";

                for (var x = xMin; x <= xMax; x++)
                {
                    var c = coords.SingleOrDefault(a => a.X == x && a.Y == y);
                    if (c == null) line += "(   ,   )";
                    else           line += $"({ThreeDigit(x)},{ThreeDigit(y)}) ";
                }

                log.Write("");
                log.Write(line);
            }
        }

        private string ThreeDigit(int i)
        {
            var a = i.ToString("D2");
            return a.PadLeft(3, '0');
        }
    }
}
