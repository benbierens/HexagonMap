namespace HexagonMap
{
    public interface ICoordinate<Tc>
        where Tc : notnull
    {
        Tc X { get; }
        Tc Y { get; }
    }

    public class Coordinate<Tc> : ICoordinate<Tc>
        where Tc : notnull
    {
        public Coordinate(Tc x, Tc y)
        {
            X = x;
            Y = y;
        }

        public Tc X { get; }
        public Tc Y { get; }
    }
}
