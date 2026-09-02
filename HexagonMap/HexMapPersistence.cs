namespace HexagonMap
{
    public interface IHexMapPersistence
    {
        void Open();
        void Close();

        IHexCell? Read(HexMapCubicCoordinate coordinate);
        void Write(IHexCell cell);
        void Delete(HexMapCubicCoordinate coordinate);
        void Iterate(Action<IHexCell> onCell);
    }

    public class InMemoryHexMapPersistence : IHexMapPersistence
    {
        private readonly Dictionary<int, Dictionary<int, IHexCell>> cells = new();

        public void Open()
        {
        }

        public void Close()
        {
        }

        public IHexCell? Read(HexMapCubicCoordinate c)
        {
            if (!cells.ContainsKey(c.Q)) return null;
            var dim = cells[c.Q];
            if (dim.TryGetValue(c.R, out var cell))
            {
                return cell;
            }
            return null;
        }

        public void Write(IHexCell cell)
        {
            var c = cell.AsCubic;
            if (!cells.ContainsKey(c.Q)) cells.Add(c.Q, new Dictionary<int, IHexCell>());
            cells[c.Q][c.R] = cell;
        }

        public void Delete(HexMapCubicCoordinate c)
        {
            if (!cells.ContainsKey(c.Q)) return;
            cells[c.Q].Remove(c.R);
        }

        public void Iterate(Action<IHexCell> onCell)
        {
            foreach (var map in cells) foreach (var cell in map.Value) onCell(cell.Value);
        }
    }
}
