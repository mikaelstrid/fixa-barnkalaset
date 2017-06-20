using System;
using System.Collections.Generic;
using System.Linq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Events;
using Xunit;
using FluentAssertions;

namespace Core.Tests
{
    public class CityAggregateTests
    {
        [Fact]
        public void Hydrate_GivenOnlyCityCreated_ShouldSetOnlyId()
        {
            // ARRANGE
            var id = Guid.Parse("2D5187EB-8ACF-4559-AEAC-D05AF7A452D5");
            var events = new List<IEvent>
            {
                new CityCreated(id, "SLUG", "NAME", 10.1, 11.2)
            };

            // ACT
            var result = new CityAggregate(events);

            // ASSERT
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void Create_GivenCorrectInput_ShouldCreateEventCityCreated()
        {
            // ARRANGE
            var id = Guid.Parse("2D5187EB-8ACF-4559-AEAC-D05AF7A452D5");
            var name = "Halmstad";
            var slug = "halmstad";
            var latitude = 10.1;
            var longitude = 12.2;
            var sut = new CityAggregate();

            // ACT
            sut.Create(id, name, slug, latitude, longitude);

            // ASSERT
            var uncommittedEvents = sut.GetUncommittedEvents();
            Assert.Equal(1, uncommittedEvents.Count());
            Assert.IsType<CityCreated>(uncommittedEvents.First());
            uncommittedEvents.First().As<CityCreated>().ShouldBeEquivalentTo(new CityCreated(id, name, slug, latitude, longitude));
        }

        [Fact]
        public void Create_GivenCorrectInputTwice_ShouldThrowInvalidOperationException()
        {
            // ARRANGE
            var id = Guid.Parse("2D5187EB-8ACF-4559-AEAC-D05AF7A452D5");
            var name = "Halmstad";
            var slug = "halmstad";
            var latitude = 10.1;
            var longitude = 12.2;
            var sut = new CityAggregate();
            sut.Create(id, name, slug, latitude, longitude);

            // ACT
            Assert.Throws<InvalidOperationException>(() => sut.Create(id, name, slug, latitude, longitude));

            // ASSERT
        }

        [Fact]
        public void Create_GivenEmptyId_ShouldThrowArgumentException()
        {
            // ARRANGE
            var id = Guid.Empty;
            var sut = new CityAggregate();

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => sut.Create(id, "Halmstad", "halmstad", 10.1, 12.1));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_GivenInvalidName_ShouldThrowArgumentException(string name)
        {
            // ARRANGE
            var sut = new CityAggregate();

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => sut.Create(Guid.NewGuid(), name, "halmstad", 10.1, 12.1));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("a b")]
        [InlineData("borås")]
        [InlineData("test?")]
        [InlineData("Boras")]
        [InlineData("   ")]
        public void Create_GivenInvalidSlug_ShouldThrowArgumentException(string slug)
        {
            // ARRANGE
            var sut = new CityAggregate();

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => sut.Create(Guid.NewGuid(), "Halmstad", slug, 10.1, 12.1));
        }

        [Fact]
        public void Create_GivenTooSmallLatitude_ShouldThrowArgumentException()
        {
            // ARRANGE
            var latitude = -90.001;
            var sut = new CityAggregate();

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => sut.Create(Guid.NewGuid(), "Halmstad", "halmstad", latitude, 12.1));
        }

        [Fact]
        public void Create_GivenTooLargeLatitude_ShouldThrowArgumentException()
        {
            // ARRANGE
            var latitude = 90.001;
            var sut = new CityAggregate();

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => sut.Create(Guid.NewGuid(), "Halmstad", "halmstad", latitude, 12.1));
        }

        [Fact]
        public void Create_GivenTooSmallLongitude_ShouldThrowArgumentException()
        {
            // ARRANGE
            var longitude = -180.001;
            var sut = new CityAggregate();

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => sut.Create(Guid.NewGuid(), "Halmstad", "halmstad", 58.1, longitude));
        }

        [Fact]
        public void Create_GivenTooLargeLongitude_ShouldThrowArgumentException()
        {
            // ARRANGE
            var longitude = 180.001;
            var sut = new CityAggregate();

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => sut.Create(Guid.NewGuid(), "Halmstad", "halmstad", 58.1, longitude));
        }

    }
}
