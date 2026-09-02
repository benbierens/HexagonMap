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
    }
}
