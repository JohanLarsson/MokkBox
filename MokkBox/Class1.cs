namespace MokkBox
{
    using System;

    using Moq;
    using NUnit.Framework;

    // Using explicit types and no vars for clearity
    public class Tests
    {
        [Test]
        public void CallMethodNoSetupReturnsDefault()
        {
            IServer server = Mock.Of<IServer>();
            Assert.IsNull(server.Login("meh"));
        }

        [Test]
        public void CallMethodWithSetupReturnsDefault()
        {
            Mock<IServer> serverMock = new Mock<IServer>();
            serverMock.Setup(x => x.Login("meh"))
                      .Returns("yeah");

            IServer server = serverMock.Object;
            var actual = server.Login("meh");
            Assert.AreEqual("yeah", actual);
            serverMock.Verify(x => x.Login("meh"), Times.Once);
            serverMock.Verify(x => x.Login(It.IsAny<string>()), Times.Once);
            serverMock.Verify(x => x.Login("aif"), Times.Never);
        }

        [Test]
        public void SetupThrow()
        {
            Mock<IServer> serverMock = new Mock<IServer>();
            serverMock.Setup(x => x.Login("meh"))
                       .Throws<ArgumentException>();
            IServer server = serverMock.Object;
            Assert.Throws<ArgumentException>(() => server.Login("meh"));
        }

        [Test]
        public void CallMethodNoSetupStrictThrows()
        {
            Mock<IServer> serverMock = new Mock<IServer>(MockBehavior.Strict);
            IServer server = serverMock.Object;
            Assert.Throws<MockException>(() => server.Login("meh"));
        }

        [Test]
        public void SetUpPropertyShort()
        {
            IServer server = Mock.Of<IServer>(x => x.SomeValue == 2);
            Assert.AreEqual(2, server.SomeValue);
        }

        [Test]
        public void SetUpPropertyLong()
        {
            Mock<IServer> serverMock = new Mock<IServer>();
            serverMock.SetupGet(x => x.SomeValue).Returns(2);
            IServer server = serverMock.Object; // three lines are the same as Mock.Of<IServer>(x => x.SomeValue == 2);
            Assert.AreEqual(2, server.SomeValue);
        }
    }

    public interface IServer
    {
        int SomeValue { get; }

        string Login(string token);
    }
}
