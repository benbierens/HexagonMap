using HexagonMapTests.Utils;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace HexagonMapTests
{
    [TestFixture]
    public class AreaTests : CellBaseTest
    {
        private readonly AreaTestSet[] OneStepSets = [
            new AreaTestSet(1, centerQR: (0, 0), expectedAreaQR: [(0, 0), (0, -1), (1, -1), (1, 0), (0, 1), (-1, 1), (-1, 0)]),
            new AreaTestSet(1, centerQR: (1, 0), expectedAreaQR: [(1, 0), (1, -1), (2, -1), (2, 0), (1, 1), (0, 1), (0, 0)]),
            new AreaTestSet(1, centerQR: (1, 1), expectedAreaQR: [(1, 1), (1, 0), (2, 0), (2, 1), (1, 2), (0, 2), (0, 1)])
        ];

        [Test]
        public void ConfirmCalculatedTestValues()
        {
            foreach (var set in OneStepSets)
            {
                var calculated = CreateTestSet(1, set.CenterQR);

                CollectionAssert.AreEquivalent(set.ExpectedAreaQR, calculated.ExpectedAreaQR);
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void CreateAreaOfOne(int testSet)
        {
            var set = OneStepSets[testSet];
            var start = Map.FromCubic(set.CenterQR);

            var area = start.GetArea(set.Range);

            CollectionAssert.AreEquivalent(set.ExpectedAreaQR, area.Select(c => c.AsCubic));
        }

        [Test]
        [Combinatorial]
        public void LargeAreaTests(
            [Values(-5, -2, 0, 6, 7)] int centerQ,
            [Values(-5, -2, 0, 6, 7)] int centerR,
            [Values(1, 3, 4, 12, 23)] int range
        )
        {
            var expectedSet = CreateTestSet(range, (centerQ, centerR));

            var start = Map.FromCubic(centerQ, centerR);

            var area = start.GetArea(range);

            CollectionAssert.AreEquivalent(expectedSet.ExpectedAreaQR, area.Select(c => c.AsCubic));
        }

        private AreaTestSet CreateTestSet(int range, (int, int) centerQR)
        {
            return new AreaTestSet(range, centerQR,
                expectedAreaQR:
                    CalculateExpectedOriginArea(range)
                    .Select(qr => OffsetByCenter(qr, centerQR))
                    .ToArray()
            );
        }

        private (int, int) OffsetByCenter((int, int) qr, (int, int) centerQR)
        {
            var q = qr.Item1 + centerQR.Item1;
            var r = qr.Item2 + centerQR.Item2;
            return (q, r);
        }

        private (int, int)[] CalculateExpectedOriginArea(int range)
        {
            var result = new List<(int, int)>();
            // given a range of 1, we go over all combinations of:
            // q = -1, 0, 1
            // r = -1, 0, 1
            // s = -1, 0, 1
            // and we include the cells where
            // q + r + s == 0

            for (var q = -range; q <= range; q++)
            {
                for (var r = -range; r <= range; r++)
                {
                    for (var s = -range; s <= range; s++)
                    {
                        if ((q + r + s) == 0) result.Add((q, r));
                    }
                }
            }
            return result.ToArray();
        }

        public class AreaTestSet
        {
            public AreaTestSet(int range, (int, int) centerQR, (int, int)[] expectedAreaQR)
            {
                Range = range;
                CenterQR = centerQR;
                ExpectedAreaQR = expectedAreaQR;
            }

            public int Range { get; }
            public (int, int) CenterQR { get; }
            public (int, int)[] ExpectedAreaQR { get; }
        }
    }
}
