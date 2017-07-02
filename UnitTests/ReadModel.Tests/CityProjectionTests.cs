using System;
using Moq;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Xunit;

namespace UnitTests.ReadModel.Tests
{
    public class CityProjectionTests
    {
        private readonly Mock<IViewRepository> _mockViewRepository;
        private readonly CityProjection _sut;

        public CityProjectionTests()
        {
            _mockViewRepository = new Mock<IViewRepository>();
            _sut = new CityProjection(_mockViewRepository.Object);
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
            _mockViewRepository.Verify(m => m.Add(It.Is<CityView>(c =>
                c.Id == id
                && c.Name == name
                && c.Slug == slug
                && c.Latitude == latitude
                && c.Longitude == longitude
            )));
        }

        [Fact]
        public void Handle_GivenCityNameChanged_ShouldUpdateTheView()
        {
            // ARRANGE
            var @event = new CityNameChanged(Guid.Parse("1927A790-FDFE-41E1-988F-E7EEC2E20D54"), "Halmstad II", "Halmstad");

            // ACT 
            _sut.Handle(@event);

            // ASSERT
            _mockViewRepository.Verify(m => m.Update(
                @event.Id,
                It.IsAny<Action<CityView>>()
            ));
        }

        [Fact]
        public void Handle_GivenCitySlugChanged_ShouldUpdateTheView()
        {
            // ARRANGE
            var @event = new CitySlugChanged(Guid.Parse("1927A790-FDFE-41E1-988F-E7EEC2E20D54"), "halmstad-ii", "halmstad");

            // ACT 
            _sut.Handle(@event);

            // ASSERT
            _mockViewRepository.Verify(m => m.Update(
                @event.Id,
                It.IsAny<Action<CityView>>()
            ));
        }

        [Fact]
        public void Handle_GivenCityPositionChanged_ShouldUpdateTheView()
        {
            // ARRANGE
            var @event = new CityPositionChanged(Guid.Parse("1927A790-FDFE-41E1-988F-E7EEC2E20D54"), 10.1, 10.2, 10.3, 10.4);

            // ACT 
            _sut.Handle(@event);

            // ASSERT
            _mockViewRepository.Verify(m => m.Update(
                @event.Id,
                It.IsAny<Action<CityView>>()
            ));
        }
    }
}
