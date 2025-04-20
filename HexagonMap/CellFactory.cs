namespace HexagonMap
{
    public interface IUserCellFactory<Tc, TuserCell>
        where Tc : notnull
        where TuserCell : class, IHasHexCell<Tc>
    {
        TuserCell CreateCell(IHexCell<Tc> hexCell);
    }

    public class EmptyUserCellFactory<Tc> : IUserCellFactory<Tc, BaseUserCell<Tc>>
        where Tc : notnull
    {
        public BaseUserCell<Tc> CreateCell(IHexCell<Tc> hexCell)
        {
            return new BaseUserCell<Tc>(hexCell);
        }
    }

    public class BaseUserCell<Tc> : IHasHexCell<Tc>
        where Tc : notnull
    {
        public BaseUserCell(IHexCell<Tc> hexCell)
        {
            HexCell = hexCell;
        }

        public IHexCell<Tc> HexCell { get; }
    }
}
