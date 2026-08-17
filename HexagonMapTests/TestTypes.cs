using HexagonMap;

namespace HexagonMapTests
{
    public class TestCell : IHasHexCell
    {
        public required IHexCell HexCell { get; init; }

        public string TestData { get; set; } = string.Empty;
    }
}
