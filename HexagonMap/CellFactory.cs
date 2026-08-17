namespace HexagonMap
{
    public interface IUserCellFactory<TUserCell, TUserCreateContext>
        where TUserCell : class, IHasHexCell
        where TUserCreateContext : notnull
    {
        TUserCell CreateCell(IHexCell hexCell, TUserCreateContext createContext);
    }
}
