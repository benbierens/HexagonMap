using HexagonMap;
using HexagonMapTests.Utils;
using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class NeighborsTests : CellBaseTest
    {
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
                here = here.Neighbors[direction].GetCell();
            }

            var back = direction.Reverse;
            // We take 'steps' steps back.
            for (var i = 0; i < steps; i++)
            {
                here = here.Neighbors[back].GetCell();
            }

            Assert.That(here, Is.SameAs(Cell));
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
                here = here.Neighbors[direction].GetCell();
            }
            Assert.That(here, Is.SameAs(Cell));
        }
    }
}
