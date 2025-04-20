using System.Diagnostics;

namespace HexagonMap
{
    public interface IHexMap<Tc, TuserCell>
        where Tc : notnull
        where TuserCell : class, IHasHexCell<Tc>
    {
        TuserCell GetCell(ICoordinate<Tc> coordinate);
    }

    public class HexMap<Tc, TuserCell> : IHexMap<Tc, TuserCell>
        where Tc : notnull
        where TuserCell : class, IHasHexCell<Tc>
    {
        private readonly IHexMapPersistence<Tc, TuserCell> persistence;
        private readonly IUserCellFactory<Tc, TuserCell> factory;
        private readonly ICoordinateTransformer<Tc> transformer;

        public HexMap(
            IHexMapPersistence<Tc, TuserCell> persistence,
            IUserCellFactory<Tc, TuserCell> factory,
            ICoordinateTransformer<Tc> transformer
        )
        {
            this.persistence = persistence;
            this.factory = factory;
            this.transformer = transformer;
        }

        public TuserCell GetCell(ICoordinate<Tc> coordinate)
        {
            var c = transformer.Transform(coordinate);
            var cell = persistence.Read(p);
            if (cell == null)
            {
                var hexCell = new HexCell<Tc>(p);
                cell = factory.CreateCell(hexCell);
                Debug.Assert(cell.HexCell == hexCell,
                    "HexCell provided to factory method was not used " +
                    "for field 'HexCell' of user type.");
            }
            return cell;
        }
    }
}
