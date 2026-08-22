namespace HexagonMap
{
    public interface ILog
    {
        void Write(string line);
    }

    public class DoNothingLog : ILog
    {
        public void Write(string line)
        {
        }
    }
}
