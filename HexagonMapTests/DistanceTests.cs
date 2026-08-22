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
            var fix = Map.GetCell(sX, sY);
            Direction direction = d;
            var target = fix.Neighbors[direction].GetCell();
            var distanceToTarget = fix.GetDistance(target);
            var distanceBack = target.GetDistance(fix);
            Assert.That(distanceToTarget, Is.EqualTo(1));
            Assert.That(distanceBack, Is.EqualTo(distanceToTarget));

            Map.Print();
        }

        [Test]
        [Combinatorial]
        public void StepsInSingleDirectionIncreaseDistance(
            [Values(0, 1, 2, 3, 4, 5)] int d,
            [Values(1, 3, 5)] int steps
        )
        {
            Direction direction = d;
            var start = Map.GetCell(0, 0);
            var here = start;

            for (var i = 0; i < steps; i++)
            {
                here = here.Neighbors[direction].GetCell();
            }

            var distance = start.GetDistance(here);

            TestContext.Error.WriteLine($"{here} -> {start} = {distance} == {steps}");
            Map.Print();

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
            var start = Map.GetCell(0, 0);
            var here = start;

            for (var i = 0; i < steps; i++)
            {
                var select = i % 2 == 0 ? d1 : d2;
                here = here.Neighbors[select].GetCell();

                var distance = start.GetDistance(here);
                Assert.That(distance, Is.EqualTo(i + 1));
            }
        }
    }
}
