using HexagonMap;
using Moq;
using NUnit.Framework;

namespace HexagonMapTests
{
    public abstract class BaseTest
    {
        protected Mock<IUserCellFactory<TestCell>> Factory { get; private set; } = null!;
        protected Mock<IHexMapPersistence<TestCell>> Persistence { get; private set; } = null!;
        protected InMemoryHexMapPersistence<TestCell> MapData { get; private set; } = null!;
        protected HexMap<TestCell> Map { get; private set; } = null!;

        [SetUp]
        public void Setup()
        {
            MapData = new InMemoryHexMapPersistence<TestCell>();

            Factory = new Mock<IUserCellFactory<TestCell>>();
            Factory.Setup(f => f.CreateCell(It.IsAny<IHexCell>())).Callback(new Func<IHexCell, TestCell>(c =>
            {
                return new TestCell { HexCell = c };
            }));

            Persistence = new Mock<IHexMapPersistence<TestCell>>();
            Persistence.Setup(f => f.Open()).Callback(() => MapData.Open());
            Persistence.Setup(f => f.Close()).Callback(() => MapData.Close());
            
            Persistence.Setup(f => f.Read(It.IsAny<HexMapCoordinate>())).Callback(new Func<HexMapCoordinate, TestCell?>(c =>
            {
                return MapData.Read(c);
            }));
            
            Persistence.Setup(f => f.Write(It.IsAny<TestCell>())).Callback(new Action<TestCell>(c =>
            {
                MapData.Write(c);
            }));

            Map = new HexMap<TestCell>(Persistence.Object, Factory.Object);
        }

        [TearDown]
        public void Teardown()
        {
            Map = null!;
            Persistence = null!;
            Factory = null!;
            MapData = null!;
        }
    }
}
