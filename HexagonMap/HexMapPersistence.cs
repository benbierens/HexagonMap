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
            if (!cells.ContainsKey(c.AsRowColumn.Column)) return null;
            var dim = cells[c.AsRowColumn.Column];
            if (dim.TryGetValue(c.AsRowColumn.Row, out var cell))
            {
                return cell;
            }
            return null;
        }

        public void Write(IHexCell cell)
        {
            var c = cell.Coordinate;
            if (!cells.ContainsKey(c.AsRowColumn.Column)) cells.Add(c.AsRowColumn.Column, new Dictionary<int, IHexCell>());
            cells[c.AsRowColumn.Column][c.AsRowColumn.Row] = cell;
        }

        public void Delete(HexMapCoordinate c)
        {
            if (!cells.ContainsKey(c.AsRowColumn.Column)) return;
            cells[c.AsRowColumn.Column].Remove(c.AsRowColumn.Row);
        }

        public void Iterate(Action<IHexCell> onCell)
        {
            foreach (var map in cells) foreach (var cell in map.Value) onCell(cell.Value);
        }
    }
}
