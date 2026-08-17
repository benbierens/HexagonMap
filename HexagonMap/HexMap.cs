using System.Diagnostics;

namespace HexagonMap
{
    public interface IHexMap<TUserCell, TUserCreateContext>
        where TUserCell : class, IHasHexCell
        where TUserCreateContext : notnull
    {
        TUserCell CreateCell(int x, int y, TUserCreateContext createContext);
        TUserCell CreateCell(HexMapCoordinate coordinate, TUserCreateContext createContext);

        TUserCell? GetCell(int x, int y);
        TUserCell? GetCell(HexMapCoordinate coordinate);

        void DeleteCell(int x, int y);
        void DeleteCell(HexMapCoordinate coordinate);
    }

    public class HexMap<TUserCell, TUserCreateContext> : IHexMap<TUserCell, TUserCreateContext>
        where TUserCell : class, IHasHexCell
        where TUserCreateContext : notnull
    {
        private readonly IHexMapPersistence<TUserCell> persistence;
        private readonly IUserCellFactory<TUserCell, TUserCreateContext> factory;

        public HexMap(
            IHexMapPersistence<TUserCell> persistence,
            IUserCellFactory<TUserCell, TUserCreateContext> factory
        )
        {
            this.persistence = persistence;
            this.factory = factory;
        }

        public TUserCell CreateCell(int x, int y, TUserCreateContext createContext)
        {
            return CreateCell(new HexMapCoordinate(x, y), createContext);
        }

        public TUserCell CreateCell(HexMapCoordinate coordinate, TUserCreateContext createContext)
        {
            Debug.Assert(GetCell(coordinate) == null);

            var hexCell = new HexCell(coordinate);
            var cell = factory.CreateCell(hexCell, createContext);

            Debug.Assert(cell.HexCell == hexCell,
                "HexCell provided to factory method was not used " +
                "for field 'HexCell' of user type.");

            return cell;
        }

        public TUserCell? GetCell(int x, int y)
        {
            return GetCell(new HexMapCoordinate(x, y));
        }

        public TUserCell? GetCell(HexMapCoordinate c)
        {
            return persistence.Read(c);
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
