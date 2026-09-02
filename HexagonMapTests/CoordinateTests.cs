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
            var c0 = HexMapCoordinate.FromRowColumn(x, y);
            var c1 = HexMapCoordinate.FromRowColumn(x, y);

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
            var c0 = HexMapCoordinate.FromRowColumn(x, y);
            var c1 = (x, y);

            Assert.That(c0, Is.EqualTo(c1));
            Assert.That(c0.Equals(c1));
            Assert.That(c0 == c1);
        }

        [Test]
        public void FromRowColumn()
        {
            AssertCoord(HexMapCoordinate.FromRowColumn(0, 0), row: 0, column: 0, q: 0, r: 0, s: 0);
            AssertCoord(HexMapCoordinate.FromRowColumn(3, 0), row: 3, column: 0, q: -1, r: 3, s: -2);
            AssertCoord(HexMapCoordinate.FromRowColumn(0, 3), row: 0, column: 3, q: 3, r: 0, s: -3);
            AssertCoord(HexMapCoordinate.FromRowColumn(1, 4), row: 1, column: 4, q: 4, r: 1, s: -5);
            AssertCoord(HexMapCoordinate.FromRowColumn(1, 5), row: 1, column: 5, q: 5, r: 1, s: -6);
        }

        [Test]
        public void FromCubic()
        {
            AssertCoord(HexMapCoordinate.FromCubic(0, 0), row: 0, column: 0, q: 0, r: 0, s: 0);
            AssertCoord(HexMapCoordinate.FromCubic(-1, 3), row: 3, column: 0, q: -1, r: 3, s: -2);
            AssertCoord(HexMapCoordinate.FromCubic(3, 0), row: 0, column: 3, q: 3, r: 0, s: -3);
            AssertCoord(HexMapCoordinate.FromCubic(4, 1), row: 1, column: 4, q: 4, r: 1, s: -5);
            AssertCoord(HexMapCoordinate.FromCubic(5, 1), row: 1, column: 5, q: 5, r: 1, s: -6);
        }

        private void AssertCoord(HexMapCoordinate c, int row, int column, int q, int r, int s)
        {
            Assert.That(c.AsRowColumn.Row, Is.EqualTo(row));
            Assert.That(c.AsRowColumn.Column, Is.EqualTo(column));

            Assert.That(c.AsCubic.Q, Is.EqualTo(q));
            Assert.That(c.AsCubic.R, Is.EqualTo(r));
            Assert.That(c.AsCubic.S, Is.EqualTo(s));
        }
    }
}
