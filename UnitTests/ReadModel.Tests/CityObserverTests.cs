using System;
using Moq;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Xunit;

namespace UnitTests.ReadModel.Tests
{
    public class CityObserverTests
    {
        private readonly Mock<IViewRepository> _mockProjectionWriter;
        private readonly CityProjection _sut;

        public CityObserverTests()
        {
            _mockProjectionWriter = new Mock<IViewRepository>();
            _sut = new CityProjection(_mockProjectionWriter.Object);
        }
        
        [Fact]
        public void Handle_GivenCityCreatedEvent_ShouldAddAView()
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
            _mockProjectionWriter.Verify(m => m.Add(It.Is<CityView>(c =>
                c.Id == id
                && c.Name == name
                && c.Slug == slug
                && c.Latitude == latitude
                && c.Longitude == longitude
            )));
        }
    }
}
