namespace HexagonMap
{
    public interface ICoordinateTransformer<Tc>
        where Tc : notnull
    {
        ICoordinate<Tc> Transform(ICoordinate<Tc> input);
    }

    public class DoNothingCoordinateTransformer<Tc> : ICoordinateTransformer<Tc>
        where Tc : notnull
    {
        public ICoordinate<Tc> Transform(ICoordinate<Tc> input)
        {
            return input;
        }
    }
}
