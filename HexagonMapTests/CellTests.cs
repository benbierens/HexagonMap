using HexagonMap;
using HexagonMapTests.Utils;
using Moq;
using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class CellTests : BaseTest
    {
        [Test]
        public void CanCreateTestCell()
        {
            var cell = Map.GetCell(0, 0);

            Assert.That(cell, Is.Not.Null);

            Persistence.Verify(p => p.Read(It.IsAny<HexMapCoordinate>()), Times.Once());
            Persistence.Verify(p => p.Write(cell), Times.Once());
        }

        [Test]
        public void KeepsCreatedCell()
        {
            Map.GetCell(0, 0);

            var cell = Map.GetCell(0, 0);

            Assert.That(cell, Is.Not.Null);

            Persistence.Verify(p => p.Read(It.IsAny<HexMapCoordinate>()), Times.Exactly(2));
            Persistence.Verify(p => p.Write(cell), Times.Once());
        }

        [Test]
        [Combinatorial]
        public void CellHasCoordinate(
            [Values(-3, -1, 0, 1, 5)] int x,
            [Values(-3, -1, 0, 1, 5)] int y
        )
        {
            var cell = Map.GetCell(x, y);

            Assert.That(cell.Coordinate.X, Is.EqualTo(x));
            Assert.That(cell.Coordinate.Y, Is.EqualTo(y));
        }
    }
}
