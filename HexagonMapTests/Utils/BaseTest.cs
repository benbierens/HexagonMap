using HexagonMap;
using Moq;
using NUnit.Framework;

namespace HexagonMapTests.Utils
{
    public abstract class BaseTest
    {
        protected Mock<IHexMapPersistence> Persistence { get; private set; } = null!;
        protected InMemoryHexMapPersistence MapData { get; private set; } = null!;
        protected HexMap Map { get; private set; } = null!;

        [SetUp]
        public void SetupBase()
        {
            MapData = new InMemoryHexMapPersistence();

            Persistence = new Mock<IHexMapPersistence>();
            Persistence.Setup(f => f.Open()).Callback(() => MapData.Open());
            Persistence.Setup(f => f.Close()).Callback(() => MapData.Close());
            
            Persistence.Setup(f => f.Read(It.IsAny<HexMapCoordinate>())).Returns(new Func<HexMapCoordinate, IHexCell?>(c =>
            {
                return MapData.Read(c);
            }));
            
            Persistence.Setup(f => f.Write(It.IsAny<IHexCell>())).Callback(new Action<IHexCell>(c =>
            {
                MapData.Write(c);
            }));

            Map = new HexMap(Persistence.Object);
        }

        [TearDown]
        public void TeardownBase()
        {
            Map = null!;
            Persistence = null!;
            MapData = null!;
        }
    }
}
