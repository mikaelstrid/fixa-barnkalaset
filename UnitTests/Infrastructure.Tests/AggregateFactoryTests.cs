using System;
using System.Collections.Generic;
using FluentAssertions;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Domain.Model;
using Pixel.FixaBarnkalaset.Infrastructure;
using Xunit;

namespace Infrastructure.Tests
{
    public class AggregateFactoryTests
    {
        [Fact]
        public void Create_GivenCityCreated_ShouldCreateAndReturnACityAggregate()
        {
            // ARRANGE
            var sut = new AggregateFactory();
            var id = Guid.Parse("C4BF9333-ECD5-4F41-9EAF-359CCA5446A9");
            var events = new List<IEvent>
            {
                new CityCreated(id, "Halmstad", "halmstad", 58.1, -127.2)
            };

            // ACT
            var result = sut.Create<CityAggregate>(events);

            // ASSERT
            result.Should().BeOfType<CityAggregate>();
            result.Id.Should().Be(id);
        }
    }
}
