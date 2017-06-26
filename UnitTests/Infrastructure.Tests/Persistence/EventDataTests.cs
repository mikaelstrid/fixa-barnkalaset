using System;
using FluentAssertions;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence;
using Xunit;

namespace UnitTests.Infrastructure.Tests.Persistence
{
    public class EventDataTests
    {
        [Fact]
        public void Serialize_Deserialize_ShouldReturnTheSameEvent()
        {
            // ARRANGE
            var id = Guid.Parse("2D5187EB-8ACF-4559-AEAC-D05AF7A452D5");
            var name = "Halmstad";
            var slug = "halmstad";
            var latitude = 10.1;
            var longitude = 12.2;
            var @event = new CityCreated(id, name, slug, latitude, longitude);

            // ACT
            var eventData = @event.ToEventData(@event.GetType().Name, @event.Id, 1);
            var result = eventData.ToEvent() as CityCreated;

            // ASSERT
            result.ShouldBeEquivalentTo(@event);
        }
    }
}
