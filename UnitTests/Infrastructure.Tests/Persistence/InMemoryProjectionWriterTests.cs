using System;
using Moq;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence;
using Pixel.FixaBarnkalaset.ReadModel;
using Xunit;

namespace UnitTests.Infrastructure.Tests.Persistence
{
    public class InMemoryProjectionWriterTests
    {
        [Fact]
        public void Add_GivenNoViews_ShouldNotThrowException()
        {
            // ARRANGE
            var sut = new InMemoryProjectionWriter();
            var mockView = new Mock<IView>();
            mockView.SetupGet(m => m.Id).Returns(Guid.Parse("F48B5459-DAEF-43F2-9E16-2DBE2AEE33D4"));

            // ACT
            sut.Add(mockView.Object);

            // ASSERT
        }

        [Fact]
        public void Add_GivenOneOtherView_ShouldNotThrowException()
        {
            // ARRANGE
            var sut = new InMemoryProjectionWriter();
            var mockOtherView = new Mock<IView>();
            mockOtherView.SetupGet(m => m.Id).Returns(Guid.Parse("7238E54D-1E90-4DB1-AD8E-13588E42A4D4"));
            sut.Add(mockOtherView.Object);

            var mockView = new Mock<IView>();
            mockView.SetupGet(m => m.Id).Returns(Guid.Parse("F48B5459-DAEF-43F2-9E16-2DBE2AEE33D4"));

            // ACT
            sut.Add(mockView.Object);

            // ASSERT
        }

        [Fact]
        public void Add_GivenViewAlreadyAdded_ShouldThrowException()
        {
            // ARRANGE
            var sut = new InMemoryProjectionWriter();
            var mockView = new Mock<IView>();
            mockView.SetupGet(m => m.Id).Returns(Guid.Parse("F48B5459-DAEF-43F2-9E16-2DBE2AEE33D4"));
            sut.Add(mockView.Object);

            // ACT && ASSERT
            Assert.Throws<ArgumentException>(() => sut.Add(mockView.Object));
        }
    }
}
