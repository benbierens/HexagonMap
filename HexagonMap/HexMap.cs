using System.Diagnostics;

namespace HexagonMap
{
    public interface IHexMap<TuserCell>
        where TuserCell : class, IHasHexCell
    {
        TuserCell GetCell(int x, int y);
        TuserCell GetCell(HexMapCoordinate coordinate);
    }

    public class HexMap<TuserCell> : IHexMap<TuserCell>
        where TuserCell : class, IHasHexCell
    {
        private readonly IHexMapPersistence<TuserCell> persistence;
        private readonly IUserCellFactory<TuserCell> factory;

        public HexMap(
            IHexMapPersistence<TuserCell> persistence,
            IUserCellFactory<TuserCell> factory
        )
        {
            this.persistence = persistence;
            this.factory = factory;
        }

        public TuserCell GetCell(int x, int y)
        {
            return GetCell(new HexMapCoordinate(x, y));
        }

        public TuserCell GetCell(HexMapCoordinate c)
        {
            var cell = persistence.Read(c);
            if (cell == null)
            {
                var hexCell = new HexCell(c);
                cell = factory.CreateCell(hexCell);
                Debug.Assert(cell.HexCell == hexCell,
                    "HexCell provided to factory method was not used " +
                    "for field 'HexCell' of user type.");
            }
            return cell;
        }
    }
}
