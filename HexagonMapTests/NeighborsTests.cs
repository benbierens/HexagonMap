using HexagonMap;
using HexagonMapTests.Utils;
using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class NeighborsTests : BaseTest
    {
        private IHexCell cell = null!;

        [SetUp]
        public void Setup()
        {
            cell = Map.GetCell(0, 0);
        }

        [TearDown]
        public void Teardown()
        {
            cell = null!;
        }

        [Test]
        [Combinatorial]
        public void LineSanityCheck(
            [Values(0, 1, 2, 3, 4, 5)] int d,
            [Values(1, 3, 5)] int steps
        )
        {
            Direction direction = d;
            var here = cell;

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

            Assert.That(here, Is.SameAs(cell));
        }

        [Test]
        [Combinatorial]
        public void CircleSanityCheck(
            [Values(0, 1, 2, 3, 4, 5)] int offset
        )
        {
            var here = cell;
            for (var i = 0; i < 6; i++)
            {
                var direction = new Direction((i + offset) % 6);
                here = here.Neighbors[direction].GetCell();
            }
            Assert.That(here, Is.SameAs(cell));
        }
    }
}
