using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class UserCellDataTests : BaseTest
    {
        [Test]
        public void CanCreateTestCell()
        {
            var cell = Map.GetCell(0, 0);

            Assert.That(cell, Is.Not.Null);
        }

        [Test]
        [Combinatorial]
        public void CellHasCoordinate(
            [Values(-3, -1, 0, 1, 5)] int x,
            [Values(-3, -1, 0, 1, 5)] int y
        )
        {
            var cell = Map.GetCell(x, y);

            Assert.That(cell.HexCell.Coordinate.X, Is.EqualTo(x));
            Assert.That(cell.HexCell.Coordinate.Y, Is.EqualTo(y));
        }
    }
}
