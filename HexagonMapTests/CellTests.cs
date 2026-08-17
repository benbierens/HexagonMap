using NUnit.Framework;

namespace HexagonMapTests
{
    [TestFixture]
    public class CellTests : BaseTest
    {
        [Test]
        public void CanCreateTestCell()
        {
            var cell = Map.CreateCell(0, 0, new TestCreateContext());

            Assert.That(cell, Is.Not.Null);
        }

        [Test]
        public void KeepsCreatedCell()
        {
            Map.CreateCell(0, 0, new TestCreateContext());

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
            var cell = Map.CreateCell(x, y, new TestCreateContext());

            Assert.That(cell.HexCell.Coordinate.X, Is.EqualTo(x));
            Assert.That(cell.HexCell.Coordinate.Y, Is.EqualTo(y));
        }

        [Test]
        public void CellHasUserData()
        {
            var context = new TestCreateContext
            {
                NewCellTestData = Guid.NewGuid().ToString()
            };

            var cell = Map.CreateCell(0, 0, context);
            Assert.That(cell.TestData, Is.EqualTo(context.NewCellTestData));

            var getCell = Map.GetCell(0, 0)!;
            Assert.That(getCell.TestData, Is.EqualTo(context.NewCellTestData));
        }
    }
}
