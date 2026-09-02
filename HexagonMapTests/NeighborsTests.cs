using HexagonMap;
using HexagonMapTests.Utils;
using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class NeighborsTests : CellBaseTest
    {
        [Test]
        [TestCase(0, -1, -1)]
        [TestCase(1, -1, 0)]
        [TestCase(2, 0, 1)]
        [TestCase(3, 1, 0)]
        [TestCase(4, 1, -1)]
        [TestCase(5, 0, -1)]
        public void OriginDirections(int direction, int row, int column)
        {
            var here = Map.FromRowColumn(0, 0);

            var step = here.GetNeighbor(direction);

            var rowColumn = step.AsRowColumn;
            Assert.That(rowColumn.Row, Is.EqualTo(row));
            Assert.That(rowColumn.Column, Is.EqualTo(column));
        }

        [Test]
        [TestCase(0, 0, 1)]
        [TestCase(1, 0, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(3, 2, 2)]
        [TestCase(4, 2, 1)]
        [TestCase(5, 1, 0)]
        public void ShiftDirections(int direction, int row, int column)
        {
            var here = Map.FromRowColumn(1, 1);

            var step = here.GetNeighbor(direction);

            var rowColumn = step.AsRowColumn;
            Assert.That(rowColumn.Row, Is.EqualTo(row));
            Assert.That(rowColumn.Column, Is.EqualTo(column));
        }

        [Test]
        [Combinatorial]
        public void LineSanityCheck(
            [Values(0, 1, 2, 3, 4, 5)] int d,
            [Values(1, 3, 5)] int steps
        )
        {
            Direction direction = d;
            var here = Cell;

            // We take 'steps' steps in direction 'direction'.
            for (var i = 0; i < steps; i++)
            {
                here = here.GetNeighbor(direction);
            }

            var back = direction.Reverse;
            // We take 'steps' steps back.
            for (var i = 0; i < steps; i++)
            {
                here = here.GetNeighbor(back);
            }

            Assert.That(here, Is.SameAs(Cell));
            Assert.Fail();
        }

        [Test]
        [Combinatorial]
        public void CircleSanityCheck(
            [Values(0, 1, 2, 3, 4, 5)] int offset
        )
        {
            var here = Cell;
            for (var i = 0; i < 6; i++)
            {
                var direction = new Direction((i + offset) % 6);
                here = here.GetNeighbor(direction);
            }
            Assert.That(here, Is.SameAs(Cell));
        }
    }
}
