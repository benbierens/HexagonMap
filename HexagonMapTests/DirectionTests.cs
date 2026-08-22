using HexagonMap;
using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class DirectionTests
    {
        [Test]
        public void Reverses()
        {
            Assert.That(D(0).Reverse, Is.EqualTo(D(3)));
            Assert.That(D(1).Reverse, Is.EqualTo(D(4)));
            Assert.That(D(2).Reverse, Is.EqualTo(D(5)));
            Assert.That(D(3).Reverse, Is.EqualTo(D(0)));
            Assert.That(D(4).Reverse, Is.EqualTo(D(1)));
            Assert.That(D(5).Reverse, Is.EqualTo(D(2)));
        }

        [Test]
        public void Clockwise()
        {
            Assert.That(D(0).Clockwise, Is.EqualTo(D(1)));
            Assert.That(D(1).Clockwise, Is.EqualTo(D(2)));
            Assert.That(D(2).Clockwise, Is.EqualTo(D(3)));
            Assert.That(D(3).Clockwise, Is.EqualTo(D(4)));
            Assert.That(D(4).Clockwise, Is.EqualTo(D(5)));
            Assert.That(D(5).Clockwise, Is.EqualTo(D(0)));
        }

        [Test]
        public void CounterClockwise()
        {
            Assert.That(D(0).CounterClockwise, Is.EqualTo(D(5)));
            Assert.That(D(1).CounterClockwise, Is.EqualTo(D(0)));
            Assert.That(D(2).CounterClockwise, Is.EqualTo(D(1)));
            Assert.That(D(3).CounterClockwise, Is.EqualTo(D(2)));
            Assert.That(D(4).CounterClockwise, Is.EqualTo(D(3)));
            Assert.That(D(5).CounterClockwise, Is.EqualTo(D(4)));
        }

        private Direction D(int v)
        {
            return new Direction(v);
        }
    }
}
