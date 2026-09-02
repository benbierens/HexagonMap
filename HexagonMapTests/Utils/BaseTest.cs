using HexagonMap;
using Moq;
using NUnit.Framework;

namespace HexagonMapTests.Utils
{
    public abstract class BaseTest
    {
        protected Mock<IHexMapPersistence> Persistence { get; private set; } = null!;
        protected InMemoryHexMapPersistence MapData { get; private set; } = null!;
        protected IHexMap Map { get; private set; } = null!;

        [SetUp]
        public void SetupBase()
        {
            MapData = new InMemoryHexMapPersistence();

            Persistence = new Mock<IHexMapPersistence>();
            Persistence.Setup(f => f.Open()).Callback(() => MapData.Open());
            Persistence.Setup(f => f.Close()).Callback(() => MapData.Close());
            
            Persistence.Setup(f => f.Read(It.IsAny<HexMapCubicCoordinate>())).Returns(new Func<HexMapCubicCoordinate, IHexCell?>(c =>
            {
                return MapData.Read(c);
            }));
            
            Persistence.Setup(f => f.Write(It.IsAny<IHexCell>())).Callback(new Action<IHexCell>(c =>
            {
                MapData.Write(c);
            }));
            Persistence.Setup(f => f.Iterate(It.IsAny<Action<IHexCell>>())).Callback(new Action<Action<IHexCell>>(c =>
            {
                MapData.Iterate(c);
            }));

            Map = new HexMap(new TestContextErrLog(), Persistence.Object);
        }

        [TearDown]
        public void TeardownBase()
        {
            if (TestContext.CurrentContext.Result.FailCount > 0)
            {
                ((HexMap)Map).Print();
            }

            Map = null!;
            Persistence = null!;
            MapData = null!;
        }
    }

    public class TestContextErrLog : ILog
    {
        public void Write(string line)
        {
            TestContext.Error.WriteLine(line);
        }
    }
}
