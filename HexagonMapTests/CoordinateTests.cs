using HexagonMap;
using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class CoordinateTests
    {
        [Test]
        [Combinatorial]
        public void Equality(
            [Values(-1, 0, 1, 2)] int x,
            [Values(-1, 0, 1, 2)] int y
        )
        {
            var c0 = new HexMapCoordinate(x, y);
            var c1 = new HexMapCoordinate(x, y);

            Assert.That(c0, Is.EqualTo(c1));
            Assert.That(c0.Equals(c1));
            Assert.That(c0 == c1);
        }

        [Test]
        [Combinatorial]
        public void ImplicitTuple(
           [Values(-1, 0, 1, 2)] int x,
           [Values(-1, 0, 1, 2)] int y
       )
        {
            var c0 = new HexMapCoordinate(x, y);
            var c1 = (x, y);

            Assert.That(c0, Is.EqualTo(c1));
            Assert.That(c0.Equals(c1));
            Assert.That(c0 == c1);
        }
    }
}
