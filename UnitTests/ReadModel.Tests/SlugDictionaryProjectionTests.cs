using System;
using Moq;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Xunit;

namespace UnitTests.ReadModel.Tests
{
    public class SlugLookupProjectionTests
    {
        private readonly Mock<ISlugLookup> _mockSlugLookup;
        private readonly SlugLookupProjection _sut;

        public SlugLookupProjectionTests()
        {
            _mockSlugLookup = new Mock<ISlugLookup>();
            _sut = new SlugLookupProjection(_mockSlugLookup.Object);
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
            _mockSlugLookup.Verify(m => m.AddSlug(slug, id));
        }

        [Fact]
        public void Handle_GivenCitySlugChanged_ShouldUpdateTheView()
        {
            // ARRANGE
            var @event = new CitySlugChanged(Guid.Parse("1927A790-FDFE-41E1-988F-E7EEC2E20D54"), "halmstad-ii", "halmstad");

            // ACT 
            _sut.Handle(@event);

            // ASSERT
            _mockSlugLookup.Verify(m => m.RemoveSlug(@event.OldSlug));
            _mockSlugLookup.Verify(m => m.AddSlug(@event.NewSlug, @event.Id));
        }
    }
}
