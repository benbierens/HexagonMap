namespace HexagonMap
{
    public interface IHexMapPersistence<Tc, TuserCell>
        where Tc : notnull
        where TuserCell : class, IHasHexCell<Tc>
    {
        void Open();
        void Close();

        TuserCell? Read(IHexCellPosition<Tc> position);
        void Write(TuserCell cell);
    }

    public class InMemoryHexMapPersistence<Tc, TuserCell> : IHexMapPersistence<Tc, TuserCell>
        where Tc : notnull
        where TuserCell : class, IHasHexCell<Tc>
    {
        private readonly Dictionary<Tc, Dictionary<Tc, TuserCell>> cells = new();

        public void Open()
        {
        }

        public void Close()
        {
        }

        public TuserCell? Read(IHexCellPosition<Tc> position)
        {
            var c = position.Coordinate;
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
            var c = cell.HexCell.Position.Coordinate;
            if (!cells.ContainsKey(c.X)) cells.Add(c.X, new Dictionary<Tc, TuserCell>());
            cells[c.X][c.Y] = cell;
        }
    }
}
