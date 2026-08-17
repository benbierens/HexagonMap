namespace HexagonMap
{
    public interface IHexMapPersistence<TuserCell>
        where TuserCell : class, IHasHexCell
    {
        void Open();
        void Close();

        TuserCell? Read(HexMapCoordinate coordinate);
        void Write(TuserCell cell);
    }

    public class InMemoryHexMapPersistence<TuserCell> : IHexMapPersistence<TuserCell>
        where TuserCell : class, IHasHexCell
    {
        private readonly Dictionary<int, Dictionary<int, TuserCell>> cells = new();

        public void Open()
        {
        }

        public void Close()
        {
        }

        public TuserCell? Read(HexMapCoordinate c)
        {
            if (!cells.ContainsKey(c.X)) return null;
            var dim = cells[c.X];
            if (dim.TryGetValue(c.Y, out var cell))
            {
                return cell;
            }
            return null;
        }

        public void Write(TuserCell cell)
        {
            var c = cell.HexCell.Coordinate;
            if (!cells.ContainsKey(c.X)) cells.Add(c.X, new Dictionary<int, TuserCell>());
            cells[c.X][c.Y] = cell;
        }
    }
}
