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
            if (!cells.ContainsKey(position.X)) return null;
            var dim = cells[position.X];
            if (dim.TryGetValue(position.Y, out var cell))
            {
                return cell;
            }
            return null;
        }

        public void Write(TuserCell cell)
        {
            var p = cell.HexCell.Position;
            if (!cells.ContainsKey(p.X)) cells.Add(p.X, new Dictionary<Tc, TuserCell>());
            cells[p.X][p.Y] = cell;
        }
    }
}
