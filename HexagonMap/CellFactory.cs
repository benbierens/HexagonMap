namespace HexagonMap
{
    public interface IUserCellFactory<TuserCell>
        where TuserCell : class, IHasHexCell
    {
        TuserCell CreateCell(IHexCell hexCell);
    }

    public class EmptyUserCellFactory : IUserCellFactory<BaseUserCell>
    {
        public BaseUserCell CreateCell(IHexCell hexCell)
        {
            return new BaseUserCell(hexCell);
        }
    }

    public class BaseUserCell : IHasHexCell
    {
        public BaseUserCell(IHexCell hexCell)
        {
            HexCell = hexCell;
        }

        public IHexCell HexCell { get; }
    }
}
