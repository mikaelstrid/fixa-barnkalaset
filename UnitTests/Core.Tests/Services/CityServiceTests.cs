using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Core.Services;
using Pixel.FixaBarnkalaset.Domain.Commands;
using Pixel.FixaBarnkalaset.Domain.Events;
using Pixel.FixaBarnkalaset.Domain.Model;
using Xunit;

namespace UnitTests.Core.Tests.Services
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
        public async Task WhenCreateCity_ShouldCallCreate_AndSave()
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
            _mockRepository.Verify(m => m.Save(_mockCityAggregate.Object));
        }

        [Fact]
        public async Task WhenChangeCityName_ShouldCallChangeName_AndSave()
        {
            // ARRANGE
            var id = Guid.Parse("68AB830F-BD2C-4520-8D2B-F0A83D6302B7");
            var newName = "Halmstad";
            var cmd = new ChangeCityName(id, newName);

            // ACT
            await _sut.When(cmd);

            // ASSERT
            _mockCityAggregate.Verify(m => m.ChangeName(newName));
            _mockRepository.Verify(m => m.Save(_mockCityAggregate.Object));
        }

        [Fact]
        public async Task WhenChangeSlug_ShouldCallChangeSlug_AndSave()
        {
            // ARRANGE
            var id = Guid.Parse("B38F5D17-E117-4302-A349-93B9E1E7199D");
            var newSlug = "halmstad";
            var cmd = new ChangeCitySlug(id, newSlug);

            // ACT
            await _sut.When(cmd);

            // ASSERT
            _mockCityAggregate.Verify(m => m.ChangeSlug(newSlug));
            _mockRepository.Verify(m => m.Save(_mockCityAggregate.Object));
        }

        [Fact]
        public async Task WhenChangePosition_ShouldCallChangePosition_AndSave()
        {
            // ARRANGE
            var id = Guid.Parse("30094051-F715-4B4A-9374-3778CB48D2DE");
            var latitude = 10.1;
            var longitude = 78.1;
            var cmd = new ChangeCityPosition(id, latitude, longitude);

            // ACT
            await _sut.When(cmd);

            // ASSERT
            _mockCityAggregate.Verify(m => m.ChangePosition(latitude, longitude));
            _mockRepository.Verify(m => m.Save(_mockCityAggregate.Object));
        }
    }
}