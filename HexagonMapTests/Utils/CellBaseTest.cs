using HexagonMap;
using NUnit.Framework;

namespace HexagonMapTests.Utils
{
    public class CellBaseTest : BaseTest
    {
        protected IHexCell Cell { get; private set; } = null!;
        protected HexMapCoordinate InitialCoordinate { get; private set; } = null!;

        [SetUp]
        public void CellBaseSetup()
        {
            InitialCoordinate = new HexMapCoordinate(Random.Shared.Next(-10, 10), Random.Shared.Next(-10, 10));
            Cell = Map.GetCell(InitialCoordinate);
        }

        [TearDown]
        public void CellBaseTeardown()
        {
            Cell = null!;
            InitialCoordinate = null!;
        }
    }
}
