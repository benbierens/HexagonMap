namespace HexagonMap
{
    public interface IHexMap
    {
        IHexCell GetCell(int x, int y);
        IHexCell GetCell(HexMapCoordinate coordinate);
    }

    public class HexMap : IHexMap
    {
        private readonly IHexMapPersistence persistence;

        public HexMap(IHexMapPersistence persistence)
        {
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

            cell = new HexCell(this, c);
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
    }
}
