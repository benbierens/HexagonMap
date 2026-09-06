using HexagonMap;
using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class CubicCoordinateTests
    {
        [Test]
        public void CreateFromRowColumn()
        {
            var c = HexMapCubicCoordinate.FromRowColumn(0, 0);

            Assert.That(c.Q, Is.EqualTo(0));
            Assert.That(c.R, Is.EqualTo(0));
        }

        [Test]
        public void CreateFromTuple()
        {
            HexMapCubicCoordinate c = (1, 2);

            Assert.That(c.Q, Is.EqualTo(1));
            Assert.That(c.R, Is.EqualTo(2));
        }

        [Test]
        public void ConvertToTuple()
        {
            (int, int) c = new HexMapCubicCoordinate(1, 2);

            Assert.That(c.Item1, Is.EqualTo(1));
            Assert.That(c.Item2, Is.EqualTo(2));
        }

        [Test]
        public void SelfEquality()
        {
            Assert.That(
                new HexMapCubicCoordinate(2, 3),
                Is.EqualTo(
                    new HexMapCubicCoordinate(2, 3)
                )
            );
        }

        [Test]
        public void TupleEquality()
        {
            Assert.That(
                new HexMapCubicCoordinate(2, 3),
                Is.EqualTo(
                    (2, 3)
                )
            );

            Assert.That(
                (2, 3),
                Is.EqualTo(
                    new HexMapCubicCoordinate(2, 3)
                )
            );
        }
    }

    [TestFixture]
    public class RowColumnCoordinateTests
    {
        [Test]
        public void CreateFromCubicCoordinate()
        {
            var c = new HexMapRowColumnOffsetCoordinate(new HexMapCubicCoordinate(0, 0));

            Assert.That(c.Row, Is.EqualTo(0));
            Assert.That(c.Column, Is.EqualTo(0));
        }

        [Test]
        public void CreateFromTuple()
        {
            HexMapRowColumnOffsetCoordinate c = (1, 2);

            Assert.That(c.Row, Is.EqualTo(1));
            Assert.That(c.Column, Is.EqualTo(2));
        }

        [Test]
        public void ConvertToTuple()
        {
            (int, int) c = new HexMapRowColumnOffsetCoordinate(new HexMapCubicCoordinate(1, 2));

            Assert.That(c.Item1, Is.EqualTo(2));
            Assert.That(c.Item2, Is.EqualTo(2));
        }

        [Test]
        public void SelfEquality()
        {
            Assert.That(
                new HexMapRowColumnOffsetCoordinate(new HexMapCubicCoordinate(2, 3)),
                Is.EqualTo(
                    new HexMapRowColumnOffsetCoordinate(new HexMapCubicCoordinate(2, 3))
                )
            );
        }

        [Test]
        public void TupleEquality()
        {
            Assert.That(
                new HexMapRowColumnOffsetCoordinate(new HexMapCubicCoordinate(2, 3)),
                Is.EqualTo(
                    (3, 3)
                )
            );

            Assert.That(
                (3, 3),
                Is.EqualTo(
                    new HexMapRowColumnOffsetCoordinate(new HexMapCubicCoordinate(2, 3))
                )
            );
        }
    }
}
