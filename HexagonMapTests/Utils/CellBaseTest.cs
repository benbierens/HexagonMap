using HexagonMap;
using NUnit.Framework;

namespace HexagonMapTests.Utils
{
    public class CellBaseTest : BaseTest
    {
        protected IHexCell Cell { get; private set; } = null!;

        [SetUp]
        public void CellBaseSetup()
        {
            Cell = Map.FromRowColumn(Random.Shared.Next(-10, 10), Random.Shared.Next(-10, 10));
        }

        [TearDown]
        public void CellBaseTeardown()
        {
            Cell = null!;
        }
    }
}
