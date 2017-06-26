using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Core.Services;
using Pixel.FixaBarnkalaset.Domain.Commands;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Domain.Model;
using Xunit;

namespace Core.Tests.Services
{
    public class CityServiceTests
    {
        private readonly Mock<CityAggregate> _mockCityAggregate;
        private readonly Mock<IAggregateRepository> _mockRepository;
        private readonly CityService _sut;

        public CityServiceTests()
        {
            _mockCityAggregate = new Mock<CityAggregate>(new List<IEvent>());
            _mockRepository = new Mock<IAggregateRepository>();
            _mockRepository.Setup(m => m.GetById<CityAggregate>(It.IsAny<Guid>())).Returns(Task.FromResult(_mockCityAggregate.Object));
            _sut = new CityService(_mockRepository.Object);
        }

        [Fact]
        public async Task WhenCreateCity_ShouldCallCreateOnTheAggregate()
        {
            // ARRANGE
            var name = "Halmstad";
            var slug = "halmstad";
            var latitude = 10.1;
            var longitude = 12.2;
            var cmd = new CreateCity(name, slug, latitude, longitude);

            // ACT
            var result = await _sut.When(cmd);

            // ASSERT
            _mockCityAggregate.Verify(m => m.Create(It.IsAny<Guid>(), name, slug, latitude, longitude));
        }

        [Fact]
        public async Task WhenCreateCity_ShouldCallSaveOnTheRepository()
        {
            // ARRANGE
            var cmd = new CreateCity("Halmstad", "halmstad", 10.12, 58.12);

            // ACT
            var result = await _sut.When(cmd);

            // ASSERT
            _mockRepository.Verify(m => m.Save(_mockCityAggregate.Object));
        }
    }
}