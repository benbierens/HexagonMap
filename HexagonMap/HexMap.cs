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
            return GetCell(HexMapCoordinate.FromRowColumn(x, y));
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
            DeleteCell(HexMapCoordinate.FromRowColumn(x, y));
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

            var colMin = coords.Min(c => c.AsRowColumn.Column);
            var colMax = coords.Max(c => c.AsRowColumn.Column);
            var rowMin = coords.Min(c => c.AsRowColumn.Row);
            var rowMax = coords.Max(c => c.AsRowColumn.Row);

            log.Write(" - ");
            for (var row = rowMin; row <= rowMax; row++)
            {
                var line = "";
                if (row % 2 != 0) line += "    ";

                for (var col = colMin; col <= colMax; col++)
                {
                    var c = coords.SingleOrDefault(a => a.AsRowColumn.Column == col && a.AsRowColumn.Row == row);
                    if (c == null) line += "(   ,   )";
                    else           line += $"({ThreeDigit(col)},{ThreeDigit(row)}) ";
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
