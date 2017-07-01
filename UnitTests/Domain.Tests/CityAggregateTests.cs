using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Domain.Model;

namespace UnitTests.Domain.Tests
{
    public class CityAggregateTests
    {
        private readonly CityAggregate _emptySut;
        private readonly CityAggregate _createdSut;

        private readonly Guid _createdSutId;
        private readonly string _createdSutName;
        private readonly string _createdSutSlug;
        private readonly double _createdSutLatitude;
        private readonly double _createdSutLongitude;

        public CityAggregateTests()
        {
            _emptySut = CreateSut(new IEvent[0]);
            _createdSutId = Guid.Parse("E31C6D38-1B34-49F9-A6AC-6F5477D480E5");
            _createdSutName = "Varberg";
            _createdSutSlug = "varberg";
            _createdSutLatitude = 16.5;
            _createdSutLongitude = -165.6;
            _createdSut =
                CreateSut(new List<IEvent>
                {
                    new CityCreated(_createdSutId, _createdSutName, _createdSutSlug, _createdSutLatitude, _createdSutLongitude)
                });
        }

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

            // ACT
            _emptySut.Create(id, name, slug, latitude, longitude);

            // ASSERT
            var uncommittedEvents = _emptySut.GetUncommittedEvents();
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
            _emptySut.Create(id, name, slug, latitude, longitude);

            // ACT
            Assert.Throws<InvalidOperationException>(() => _emptySut.Create(id, name, slug, latitude, longitude));

            // ASSERT
        }

        [Fact]
        public void Create_GivenEmptyId_ShouldThrowArgumentException()
        {
            // ARRANGE
            var id = Guid.Empty;

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => _emptySut.Create(id, "Halmstad", "halmstad", 10.1, 12.1));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_GivenInvalidName_ShouldThrowArgumentException(string name)
        {
            // ARRANGE

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => _emptySut.Create(Guid.NewGuid(), name, "halmstad", 10.1, 12.1));
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

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => _emptySut.Create(Guid.NewGuid(), "Halmstad", slug, 10.1, 12.1));
        }

        [Theory]
        [InlineData(-90.001)]
        [InlineData(90.001)]
        public void Create_GivenInvalidLatitude_ShouldThrowArgumentException(double latitude)
        {
            // ARRANGE

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => _emptySut.Create(Guid.NewGuid(), "Halmstad", "halmstad", latitude, 12.1));
        }

        [Theory]
        [InlineData(-180.001)]
        [InlineData(180.001)]
        public void Create_GivenInvalidLongitude_ShouldThrowArgumentException(double longitude)
        {
            // ARRANGE

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => _emptySut.Create(Guid.NewGuid(), "Halmstad", "halmstad", 58.1, longitude));
        }


        [Fact]
        public void ChangeName_GivenCorrectInput_ShouldCreateEventCityNameChanged()
        {
            // ARRANGE
            var newName = "Halmstad II";

            // ACT
            _createdSut.ChangeName(newName);

            // ASSERT
            var uncommittedEvents = _createdSut.GetUncommittedEvents();
            Assert.Equal(1, uncommittedEvents.Count());
            Assert.IsType<CityNameChanged>(uncommittedEvents.First());
            uncommittedEvents.First().As<CityNameChanged>().ShouldBeEquivalentTo(new CityNameChanged(_createdSutId, newName, _createdSutName));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ChangeName_GivenInvalidName_ShouldThrowArgumentException(string newName)
        {
            // ARRANGE

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => _createdSut.ChangeName(newName));
        }


        [Fact]
        public void ChangeSlug_GivenCorrectInput_ShouldCreateEventCitySlugChanged()
        {
            // ARRANGE
            var newSlug = "halmstad-ii";

            // ACT
            _createdSut.ChangeSlug(newSlug);

            // ASSERT
            var uncommittedEvents = _createdSut.GetUncommittedEvents();
            Assert.Equal(1, uncommittedEvents.Count());
            Assert.IsType<CitySlugChanged>(uncommittedEvents.First());
            uncommittedEvents.First().As<CitySlugChanged>().ShouldBeEquivalentTo(new CitySlugChanged(_createdSutId, newSlug, _createdSutSlug));
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
        public void ChangeSlug_GivenInvalidSlug_ShouldThrowArgumentException(string newSlug)
        {
            // ARRANGE

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => _createdSut.ChangeSlug(newSlug));
        }



        [Fact]
        public void ChangePosition_GivenCorrectInput_ShouldCreateEventCityPositionChanged()
        {
            // ARRANGE
            var newLatitude = 18.89;
            var newLongitude = 89.12;

            // ACT
            _createdSut.ChangePosition(newLatitude, newLongitude);

            // ASSERT
            var uncommittedEvents = _createdSut.GetUncommittedEvents();
            Assert.Equal(1, uncommittedEvents.Count());
            Assert.IsType<CityPositionChanged>(uncommittedEvents.First());
            uncommittedEvents.First().As<CityPositionChanged>().ShouldBeEquivalentTo(new CityPositionChanged(_createdSutId, newLatitude, newLongitude, _createdSutLatitude, _createdSutLongitude));
        }

        [Theory]
        [InlineData(-90.001)]
        [InlineData(90.001)]
        public void ChangePosition_GivenInvalidLatitude_ShouldThrowArgumentException(double latitude)
        {
            // ARRANGE

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => _createdSut.ChangePosition(latitude, 58.1));
        }




        [Theory]
        [InlineData(-180.001)]
        [InlineData(180.001)]
        public void ChangePosition_GivenInvalidLongitude_ShouldThrowArgumentException(double longitude)
        {
            // ARRANGE

            // ACT

            // ASSERT
            Assert.Throws<ArgumentException>(() => _createdSut.ChangePosition(12.1, longitude));
        }



        private static CityAggregate CreateSut(IEnumerable<IEvent> events)
        {
            return new CityAggregate(events);
        }
    }
}
