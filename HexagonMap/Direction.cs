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
        private static readonly Dictionary<Direction, (int, int)> cubicTransforms = new()
        {
            { 0, (0, -1) },
            { 1, (1, -1) },
            { 2, (1, 0) },
            { 3, (0, 1) },
            { 4, (-1, 1) },
            { 5, (-1, 0) },
        };

        private readonly byte value;

        public Direction(int value)
            : this(Convert.ToByte(value))
        {
        }

        public Direction(byte value)
        {
            if (value > 5) throw new Exception($"Directional value '{value}' is out of range.");

            this.value = value;
        }

        internal HexMapCubicCoordinate TransformCubic(HexMapCubicCoordinate cubic)
        {
            var transform = cubicTransforms[this];
            return new HexMapCubicCoordinate(cubic.Q + transform.Item1, cubic.R + transform.Item2);
        }

        public override string ToString()
        {
            return value!.ToString();
        }

        public Direction Reverse => reverses[this];
        public Direction Clockwise => (value + 1) % 6;
        public Direction CounterClockwise => WrapMod(value - 1, 6);

        public static implicit operator Direction(byte value) { return new Direction(value); }
        public static implicit operator Direction(int value) { return new Direction(value); }
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

        private int WrapMod(int x, int m)
        {
            var r = x % m;
            return r < 0 ? r + m : r;
        }
    }
}
