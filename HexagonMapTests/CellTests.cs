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
            var cell = Map.FromRowColumn(0, 0);

            Assert.That(cell, Is.Not.Null);

            Persistence.Verify(p => p.Read(It.IsAny<HexMapCubicCoordinate>()), Times.Once());
            Persistence.Verify(p => p.Write(cell), Times.Once());
        }

        [Test]
        public void KeepsCreatedCell()
        {
            Map.FromRowColumn(0, 0);

            var cell = Map.FromRowColumn(0, 0);

            Assert.That(cell, Is.Not.Null);

            Persistence.Verify(p => p.Read(It.IsAny<HexMapCubicCoordinate>()), Times.Exactly(2));
            Persistence.Verify(p => p.Write(cell), Times.Once());
        }

        [Test]
        [Combinatorial]
        public void Equality(
                    [Values(-1, 0, 1, 2)] int x,
                    [Values(-1, 0, 1, 2)] int y
                )
        {
            var c0 = Map.FromRowColumn(x, y);
            var c1 = Map.FromRowColumn(x, y);

            Assert.That(c0, Is.EqualTo(c1));
            Assert.That(c0.Equals(c1));
            Assert.That(c0 == c1);
        }
        
        [Test]
        public void GetCellFromRowColumn()
        {
            AssertCoord(Map.FromRowColumn(0, 0), row: 0, column: 0, q: 0, r: 0, s: 0);
            AssertCoord(Map.FromRowColumn(3, 0), row: 3, column: 0, q: -1, r: 3, s: -2);
            AssertCoord(Map.FromRowColumn(0, 3), row: 0, column: 3, q: 3, r: 0, s: -3);
            AssertCoord(Map.FromRowColumn(1, 4), row: 1, column: 4, q: 4, r: 1, s: -5);
            AssertCoord(Map.FromRowColumn(1, 5), row: 1, column: 5, q: 5, r: 1, s: -6);
        }

        [Test]
        public void GetCellFromCubic()
        {
            AssertCoord(Map.FromCubic(0, 0), row: 0, column: 0, q: 0, r: 0, s: 0);
            AssertCoord(Map.FromCubic(-1, 3), row: 3, column: 0, q: -1, r: 3, s: -2);
            AssertCoord(Map.FromCubic(3, 0), row: 0, column: 3, q: 3, r: 0, s: -3);
            AssertCoord(Map.FromCubic(4, 1), row: 1, column: 4, q: 4, r: 1, s: -5);
            AssertCoord(Map.FromCubic(5, 1), row: 1, column: 5, q: 5, r: 1, s: -6);
        }

        private void AssertCoord(IHexCell c, int row, int column, int q, int r, int s)
        {
            Assert.That(c.AsRowColumn.Row, Is.EqualTo(row));
            Assert.That(c.AsRowColumn.Column, Is.EqualTo(column));

            Assert.That(c.AsCubic.Q, Is.EqualTo(q));
            Assert.That(c.AsCubic.R, Is.EqualTo(r));
            Assert.That(c.AsCubic.S, Is.EqualTo(s));
        }
    }
}
