namespace HexagonMap
{
    public interface IHexMapPersistence
    {
        void Open();
        void Close();

        IHexCell? Read(HexMapCoordinate coordinate);
        void Write(IHexCell cell);
        void Delete(HexMapCoordinate coordinate);
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

        public IHexCell? Read(HexMapCoordinate c)
        {
            if (!cells.ContainsKey(c.X)) return null;
            var dim = cells[c.X];
            if (dim.TryGetValue(c.Y, out var cell))
            {
                return cell;
            }
            return null;
        }

        public void Write(IHexCell cell)
        {
            var c = cell.Coordinate;
            if (!cells.ContainsKey(c.X)) cells.Add(c.X, new Dictionary<int, IHexCell>());
            cells[c.X][c.Y] = cell;
        }

        public void Delete(HexMapCoordinate c)
        {
            if (!cells.ContainsKey(c.X)) return;
            cells[c.X].Remove(c.Y);
        }

        public void Iterate(Action<IHexCell> onCell)
        {
            foreach (var map in cells) foreach (var cell in map.Value) onCell(cell.Value);
        }
    }
}
