namespace HexagonMap
{
    public interface IHexCell<Tc>
        where Tc : notnull
    {
        IHexCellPosition<Tc> Position { get; }

        string Serialize();
        static IHexCell<Tc> Deserialize(string str)
        {
            return HexCell<Tc>.Deserialize(str);
        }
    }

    public interface IHasHexCell<Tc>
        where Tc : notnull
    {
        IHexCell<Tc> HexCell { get; }
    }

    public class HexCell<Tc> : IHexCell<Tc>
        where Tc : notnull
    {
        public HexCell(IHexCellPosition<Tc> position)
        {
            Position = position;
        }

        public IHexCellPosition<Tc> Position { get; }

        public string Serialize()
        {
            throw new NotImplementedException();
        }

        public static IHexCell<Tc> Deserialize(string str)
        {
            throw new NotImplementedException();
        }
    }
}
