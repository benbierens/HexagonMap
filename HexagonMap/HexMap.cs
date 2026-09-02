namespace HexagonMap
{
    public interface IHexMap
    {
        IHexCell FromRowColumn(int x, int y);
        IHexCell FromCubic(int q, int r);
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

        public IHexCell FromRowColumn(int row, int column)
        {
            return GetCell(HexMapCubicCoordinate.FromRowColumn(row, column));
        }

        public IHexCell FromCubic(int q, int r)
        {
            return GetCell(new HexMapCubicCoordinate(q, r));
        }

        internal IHexCell GetCell(HexMapCubicCoordinate c)
        {
            var cell = persistence.Read(c);
            if (cell != null) return cell;

            cell = new HexCell(log, this, c);
            persistence.Write(cell);
            return cell;
        }

        public void DeleteCellByRowColumn(int row, int column)
        {
            persistence.Delete(HexMapCubicCoordinate.FromRowColumn(row, column));
        }

        public void DeleteCellByCubic(int q, int r)
        {
            persistence.Delete(new HexMapCubicCoordinate(q, r));
        }

        public void Print()
        {
            var cells = new List<IHexCell>();
            persistence.Iterate(cells.Add);

            var colMin = cells.Min(c => c.AsRowColumn.Column);
            var colMax = cells.Max(c => c.AsRowColumn.Column);
            var rowMin = cells.Min(c => c.AsRowColumn.Row);
            var rowMax = cells.Max(c => c.AsRowColumn.Row);

            log.Write(" - ");
            for (var row = rowMin; row <= rowMax; row++)
            {
                var line = "";
                if (row % 2 != 0) line += "    ";

                for (var col = colMin; col <= colMax; col++)
                {
                    var c = cells.SingleOrDefault(a => a.AsRowColumn.Column == col && a.AsRowColumn.Row == row);
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
