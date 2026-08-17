namespace HexagonMap
{
    public class Direction
    {
        private static readonly Dictionary<Direction, Direction> reverses = new()
        {
            { 0, 3 },
            { 1, 4 },
            { 2, 5 },
            { 3, 0 },
            { 4, 1 },
            { 5, 2 },
        };
        protected readonly byte value;

        public Direction(byte value)
        {
            if (value > 5) throw new Exception($"Directional value '{value}' is out of range.");

            this.value = value;
        }

        public override string ToString()
        {
            return value!.ToString();
        }

        public Direction Reverse => reverses[this];

        public static implicit operator Direction(byte value) { return new Direction(value); }
        public static implicit operator byte(Direction direction) { return direction.value; }

        public static bool operator <(Direction a, Direction b)
        {
            return Comparer<byte>.Default.Compare(a.value, b.value) < 0;
        }

        public static bool operator >(Direction a, Direction b)
        {
            return !(a < b);
        }

        public static bool operator <=(Direction a, Direction b)
        {
            return (a < b) || (a == b);
        }

        public static bool operator >=(Direction a, Direction b)
        {
            return (a > b) || (a == b);
        }

        public static bool operator ==(Direction a, Direction b)
        {
            return a.Equals((object)b);
        }

        public static bool operator !=(Direction a, Direction b)
        {
            return !(a == b);
        }

        protected bool Equals(Direction other)
        {
            return EqualityComparer<byte>.Default.Equals(value, other.value);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Direction)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<byte>.Default.GetHashCode(value);
        }
    }
}
