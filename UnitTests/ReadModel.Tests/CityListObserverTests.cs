using System;
using System.Linq;
using Moq;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Xunit;

namespace UnitTests.ReadModel.Tests
{
    public class CityListObserverTests
    {
        private readonly Mock<IViewRepository> _mockProjectionWriter;
        private readonly CityListProjection _sut;

        public CityListObserverTests()
        {
            _mockProjectionWriter = new Mock<IViewRepository>();
            _sut = new CityListProjection(_mockProjectionWriter.Object);
        }
        
        [Fact]
        public void Handle_GivenCityCreatedEvent_AndThisIsTheFirstEvent_ShouldAddAView()
        {
            // ARRANGE
            var id = Guid.Parse("FB90B38E-E859-4093-8044-D4223277D9DF");
            var name = "Halmstad";
            var slug = "halmstad";
            var latitude = 10.1;
            var longitude = 12.2;
            var @event = new CityCreated(id, name, slug, latitude, longitude);

            // ACT 
            _sut.Handle(@event);

            // ASSERT
            _mockProjectionWriter.Verify(m => m.Add(It.Is<CityListView>(v =>
                v.Cities.Count == 1
                && v.Cities.First().Name == name
                && v.Cities.First().Slug == slug
            )));
        }

        [Fact]
        public void Handle_GivenCityCreatedEvent_AndThisIsNotTheFirstEvent_ShouldUpdateTheView()
        {
            // ARRANGE
            var id = Guid.Parse("FB90B38E-E859-4093-8044-D4223277D9DF");
            var name = "Halmstad";
            var slug = "halmstad";
            var latitude = 10.1;
            var longitude = 12.2;
            var @event = new CityCreated(id, name, slug, latitude, longitude);
            _mockProjectionWriter.Setup(m => m.Contains<CityListView>(It.IsAny<Guid>())).Returns(true);

            // ACT 
            _sut.Handle(@event);

            // ASSERT
            _mockProjectionWriter.Verify(m => m.Update(
                It.IsAny<Guid>(),
                It.IsAny<Action<CityListView>>()
            ));
        }
    }
}
