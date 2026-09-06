using HexagonMap;
using HexagonMapTests.Utils;
using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class JumpTests : CellBaseTest
    {
        [Test]
        [TestCase(0, 2, -2, -1)]
        [TestCase(0, 3, -3, -2)]
        [TestCase(1, 2, -2, 1)]
        [TestCase(1, 3, -3, 1)]
        [TestCase(2, 2, 0, 2)]
        [TestCase(2, 3, 0, 3)]
        [TestCase(3, 2, 2, 1)]
        [TestCase(3, 3, 3, 1)]
        [TestCase(4, 2, 2, -1)]
        [TestCase(4, 3, 3, -2)]
        [TestCase(5, 2, 0, -2)]
        [TestCase(5, 3, 0, -3)]
        public void KnownJumps(
            int d,
            int steps,
            int expectedRow,
            int expectedColumn
        )
        {
            var here = Map.FromRowColumn(0, 0);
            var jump = here.Jump(d, steps);

            Assert.That(jump.AsRowColumn, Is.EqualTo((expectedRow, expectedColumn)));
        }

        [Test]
        [Combinatorial]
        public void LineSanityCheck(
            [Values(0, 1, 2, 3, 4, 5)] int d,
            [Values(1, 5, 10)] int steps
        )
        {
            Direction dir = d;
            var here = Cell.Jump(dir, steps);
            var back = here.Jump(dir.Reverse, steps);

            Assert.That(back, Is.SameAs(Cell));
        }

        [Test]
        [Combinatorial]
        public void CircleSanityCheck(
            [Values(0, 1, 2, 3, 4, 5)] int offset,
            [Values(1, 5, 10)] int steps
        )
        {
            var here = Cell;
            for (var i = 0; i < 6; i++)
            {
                var direction = new Direction((i + offset) % 6);
                here = here.Jump(direction, steps);
            }
            Assert.That(here, Is.SameAs(Cell));
        }
    }
}
