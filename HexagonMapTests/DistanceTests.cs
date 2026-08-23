using HexagonMap;
using HexagonMapTests.Utils;
using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class DistanceTests : CellBaseTest
    {
        [Test]
        public void ZeroDistance()
        {
            Assert.That(InitialCoordinate.GetDistance(new TestContextErrLog(), InitialCoordinate), Is.EqualTo(0));
        }

        [Test]
        [Combinatorial]
        public void DistancesOfOne(
            [Values(0, 1, 2)] int sX,
            [Values(0, 1, 2)] int sY,
            [Values(0, 1, 2, 3, 4, 5)] int d
        )
        {
            Direction direction = d;
            var target = Cell.Neighbors[direction].GetCell();
            var distanceToTarget = Cell.GetDistance(target);
            var distanceBack = target.GetDistance(Cell);
            Assert.That(distanceToTarget, Is.EqualTo(1));
            Assert.That(distanceBack, Is.EqualTo(distanceToTarget));
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
            }

            var distance = Cell.GetDistance(here);
            Assert.That(distance, Is.EqualTo(steps));
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
            }
            var distance = Cell.GetDistance(here);

            Assert.That(distance, Is.EqualTo(steps));
        }

        [Test]
        public void CurvedPathDistance(
            [Values(0, 1, 2, 3, 4, 5)] int d,
            [Values(1, 3, 5)] int steps)
        {
            var here = Cell;
            Direction direction = d;

            for (var i = 0; i < steps; i++) here = here.Neighbors[direction].GetCell();
            direction = direction.Clockwise;
            for (var i = 0; i < steps; i++) here = here.Neighbors[direction].GetCell();
            direction = direction.Clockwise;
            for (var i = 0; i < steps; i++) here = here.Neighbors[direction].GetCell();

            var expectedDistance = 2 * steps;
            var distance = Cell.GetDistance(here);

            Assert.That(distance, Is.EqualTo(expectedDistance));
        }
    }
}
