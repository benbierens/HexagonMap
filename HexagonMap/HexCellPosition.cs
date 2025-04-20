namespace HexagonMap
{
    public interface IHexCellPosition<Tc>
        where Tc : notnull
    {
        ICoordinate<Tc> Coordinate { get; }
        bool IsShifted { get; }
    }

    internal class HexCellPosition<Tc> : IHexCellPosition<Tc>
        where Tc : notnull
    {
        internal HexCellPosition(IMath<Tc> math, ICoordinate<Tc> coordinate)
        {
            Coordinate = coordinate;
            IsShifted = !math.IsEven(coordinate.Y);
        }

        public ICoordinate<Tc> Coordinate { get; }
        public bool IsShifted { get; }
    }

    public interface IMath<Tc>
        where Tc : notnull
    {
        bool IsEven(Tc value); // = Y % 2 == 0;
    }
}
