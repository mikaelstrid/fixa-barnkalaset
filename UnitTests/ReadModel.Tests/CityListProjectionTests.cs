using System;
using System.Linq;
using Moq;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Xunit;

namespace UnitTests.ReadModel.Tests
{
    public class CityListProjectionTests
    {
        private readonly Mock<IViewRepository> _mockViewRepository;
        private readonly CityListProjection _sut;

        public CityListProjectionTests()
        {
            _mockViewRepository = new Mock<IViewRepository>();
            _mockViewRepository.Setup(m => m.Contains<CityListView>(It.IsAny<Guid>())).Returns(true);
            _sut = new CityListProjection(_mockViewRepository.Object);
        }
        
        [Fact]
        public void Handle_GivenCityCreated_AndThisIsTheFirstEvent_ShouldAddAView()
        {
            // ARRANGE
            _mockViewRepository.Setup(m => m.Contains<CityListView>(It.IsAny<Guid>())).Returns(false);
            var id = Guid.Parse("FB90B38E-E859-4093-8044-D4223277D9DF");
            var name = "Halmstad";
            var slug = "halmstad";
            var latitude = 10.1;
            var longitude = 12.2;
            var @event = new CityCreated(id, name, slug, latitude, longitude);

            // ACT 
            _sut.Handle(@event);

            // ASSERT
            _mockViewRepository.Verify(m => m.Add(It.Is<CityListView>(v =>
                v.Id == CityListView.ListViewId 
                && v.Cities.Count == 1
                && v.Cities.First().Id == id
                && v.Cities.First().Name == name
                && v.Cities.First().Slug == slug
                && v.Cities.First().Latitude == latitude
                && v.Cities.First().Longitude == longitude
                && v.Cities.First().ArrangementsCount == 0
            )));
        }

        [Fact]
        public void Handle_GivenCityCreated_AndThisIsNotTheFirstEvent_ShouldUpdateTheView()
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
            _mockViewRepository.Verify(m => m.Update(
                It.IsAny<Guid>(),
                It.IsAny<Action<CityListView>>()
            ));
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
                It.IsAny<Guid>(),
                It.IsAny<Action<CityListView>>()
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
                It.IsAny<Guid>(),
                It.IsAny<Action<CityListView>>()
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
                It.IsAny<Guid>(),
                It.IsAny<Action<CityListView>>()
            ));
        }

    }
}
