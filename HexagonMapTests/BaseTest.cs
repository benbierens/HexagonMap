using HexagonMap;
using Moq;
using NUnit.Framework;

namespace HexagonMapTests
{
    public abstract class BaseTest
    {
        protected Mock<IUserCellFactory<TestCell, TestCreateContext>> Factory { get; private set; } = null!;
        protected Mock<IHexMapPersistence<TestCell>> Persistence { get; private set; } = null!;
        protected InMemoryHexMapPersistence<TestCell> MapData { get; private set; } = null!;
        protected HexMap<TestCell, TestCreateContext> Map { get; private set; } = null!;

        [SetUp]
        public void Setup()
        {
            MapData = new InMemoryHexMapPersistence<TestCell>();

            Factory = new Mock<IUserCellFactory<TestCell, TestCreateContext>>();
            Factory.Setup(f => f.CreateCell(It.IsAny<IHexCell>(), It.IsAny<TestCreateContext>())).Callback(new Func<IHexCell, TestCreateContext, TestCell>((cell, context) =>
            {
                return new TestCell
                { 
                    HexCell = cell,
                    TestData = context.NewCellTestData
                };
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

            Map = new HexMap<TestCell, TestCreateContext>(Persistence.Object, Factory.Object);
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
