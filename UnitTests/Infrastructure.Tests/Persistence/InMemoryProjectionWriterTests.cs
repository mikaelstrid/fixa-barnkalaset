using System;
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
            var view = new TestView();

            // ACT
            sut.Add(view);

            // ASSERT
        }

        [Fact]
        public void Add_GivenOneOtherView_ShouldNotThrowException()
        {
            // ARRANGE
            var sut = new InMemoryProjectionWriter();
            var otherView = new TestView();
            sut.Add(otherView);

            var view = new TestView();

            // ACT
            sut.Add(view);

            // ASSERT
        }

        [Fact]
        public void Add_GivenViewAlreadyAdded_ShouldThrowException()
        {
            // ARRANGE
            var sut = new InMemoryProjectionWriter();
            var view = new TestView();
            sut.Add(view);

            // ACT && ASSERT
            Assert.Throws<ArgumentException>(() => sut.Add(view));
        }


        [Fact]
        public void Update_GivenNoViews_ShouldThrowException()
        {
            // ARRANGE
            var sut = new InMemoryProjectionWriter();
            var view = new TestView();

            // ACT && ASSERT
            Assert.Throws<ArgumentException>(() => sut.Update<IView>(view.Id, v => { }));
        }

        [Fact]
        public void Update_GivenOneOtherView_ShouldThrowException()
        {
            // ARRANGE
            var sut = new InMemoryProjectionWriter();
            var otherView = new TestView();
            sut.Add(otherView);

            var view = new TestView();

            // ACT && ASSERT
            Assert.Throws<ArgumentException>(() => sut.Update<IView>(view.Id, v => { }));
        }

        [Fact]
        public void Update_GivenViewAlreadyAdded_ShouldNotThrowException()
        {
            // ARRANGE
            var sut = new InMemoryProjectionWriter();
            var view = new TestView();
            sut.Add(view);

            // ACT 
            sut.Update<TestView>(view.Id, v => {});

            // ASSERT
        }


        public class TestView : ViewBase
        {
            public TestView()
            {
                Id = Guid.NewGuid();
            }
        }
    }
}
