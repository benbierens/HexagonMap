using HexagonMap;
using HexagonMapTests.Utils;
using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class DistanceTests : CellBaseTest
    {
        [Test]
        [Combinatorial]
        public void DistancesOfOne(
            [Values(0, 1)] int s,
            [Values(0, 1, 2, 3, 4, 5)] int d
        )
        {
            var fix = Map.GetCell(s, s);
            Direction direction = d;
            var target = fix.Neighbors[direction].GetCell();
            var distance = fix.GetDistance(target);
            Assert.That(distance, Is.EqualTo(1));
        }

        [Test]
        [Combinatorial]
        public void StepsInSingleDirectionIncreaseDistance(
            [Values(0, 1, 2, 3, 4, 5)] int d,
            [Values(1, 3, 5)] int steps
        )
        {
            Direction direction = d;
            var here = Cell;

            for (var i = 0; i < steps; i++)
            {
                here = here.Neighbors[direction].GetCell();

                var distance = Cell.GetDistance(here);
                Assert.That(distance, Is.EqualTo(i + 1));
            }
        }

        [Test]
        [Combinatorial]
        public void StepsInAdjacentDirectionsIncreaseDistance(
            [Values(0, 1, 2, 3, 4, 5)] int d,
            [Values(1, 3, 5)] int steps,
            [Values(true, false)] bool clockwise
        )
        {
            Direction d1 = d;
            Direction d2 = clockwise ? d1.Clockwise : d1.CounterClockwise;
            var here = Cell;

            for (var i = 0; i < steps; i++)
            {
                var select = i % 2 == 0 ? d1 : d2;
                here = here.Neighbors[select].GetCell();

                var distance = Cell.GetDistance(here);
                Assert.That(distance, Is.EqualTo(i + 1));
            }
        }
    }
}
