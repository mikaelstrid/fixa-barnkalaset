using System;
using System.Collections.Generic;
using FluentAssertions;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence;
using Pixel.FixaBarnkalaset.ReadModel;
using Xunit;

namespace UnitTests.Infrastructure.Tests.Persistence
{
    public class InMemoryViewRepositoryTests
    {
        [Fact]
        public void Get_GivenNoViews_ShouldThrowException()
        {
            // ARRANGE
            var sut = new InMemoryViewRepository();

            // ACT && ASSERT
            Assert.Throws<KeyNotFoundException>(() => sut.Get<TestView>(Guid.NewGuid()));
        }

        [Fact]
        public void Get_GivenOneOtherView_ShouldThrowException()
        {
            // ARRANGE
            var sut = new InMemoryViewRepository();
            var otherView = new TestView();
            sut.Add(otherView);

            // ACT && ASSERT
            Assert.Throws<KeyNotFoundException>(() => sut.Get<TestView>(Guid.NewGuid()));
        }

        [Fact]
        public void Get_GivenViewAlreadyAdded_ShouldReturnTheSameView()
        {
            // ARRANGE
            var sut = new InMemoryViewRepository();
            var view = new TestView();
            sut.Add(view);

            // ACT 
            var result = sut.Get<TestView>(view.Id);

            // ASSERT
            result.Should().Be(view);
        }

        [Fact]
        public void Get_GivenOneOtherViewWithSameIdButOtherType_ShouldThrowException()
        {
            // ARRANGE
            var sut = new InMemoryViewRepository();
            var otherView = new OtherTestView();
            sut.Add(otherView);

            // ACT && ASSERT
            Assert.Throws<KeyNotFoundException>(() => sut.Get<TestView>(otherView.Id));
        }



        [Fact]
        public void Add_GivenNoViews_ShouldNotThrowException()
        {
            // ARRANGE
            var sut = new InMemoryViewRepository();
            var view = new TestView();

            // ACT
            sut.Add(view);

            // ASSERT
        }

        [Fact]
        public void Add_GivenOneOtherView_ShouldNotThrowException()
        {
            // ARRANGE
            var sut = new InMemoryViewRepository();
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
            var sut = new InMemoryViewRepository();
            var view = new TestView();
            sut.Add(view);

            // ACT && ASSERT
            Assert.Throws<ArgumentException>(() => sut.Add(view));
        }


        [Fact]
        public void Update_GivenNoViews_ShouldThrowException()
        {
            // ARRANGE
            var sut = new InMemoryViewRepository();
            var view = new TestView();

            // ACT && ASSERT
            Assert.Throws<ArgumentException>(() => sut.Update<IView>(view.Id, v => { }));
        }

        [Fact]
        public void Update_GivenOneOtherView_ShouldThrowException()
        {
            // ARRANGE
            var sut = new InMemoryViewRepository();
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
            var sut = new InMemoryViewRepository();
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

        public class OtherTestView : ViewBase
        {
            public OtherTestView()
            {
                Id = Guid.NewGuid();
            }
        }
    }
}
