using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;
using Pixel.FixaBarnkalaset.Web;
using Pixel.FixaBarnkalaset.Web.Controllers;
using Pixel.FixaBarnkalaset.Web.Models;
using Xunit;

namespace UnitTests.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
        private readonly HomeController _sut;
        private readonly IMapper _mapper;
        private readonly Mock<IViewRepository> _mockViewRepository;

        public HomeControllerTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            _mockViewRepository = new Mock<IViewRepository>();
            _sut = new HomeController(_mapper, _mockViewRepository.Object);
        }

        [Fact]
        public void Index_GivenNoCityListView_ShouldGetCititesFromTheViewRepository_AndReturnAnEmptyResponseModel()
        {
            // ARRANGE
            //_mockViewRepository.Setup(m => m.Get<CityListView>(CityListView.ListViewId)).Returns
            
            // ACT
            var result = _sut.Index();

            // ASSERT
            _mockViewRepository.Verify(m => m.Get<CityListView>(CityListView.ListViewId));
            ((result as ViewResult).Model as HomeIndexViewModel).Cities.Should().BeEmpty();
        }

        [Fact]
        public void Index_GivenCityListViewWithNoCityList_ShouldGetCititesFromTheViewRepository_AndReturnAnEmptyResponseModel()
        {
            // ARRANGE
            _mockViewRepository.Setup(m => m.Get<CityListView>(CityListView.ListViewId)).Returns(new CityListView());

            // ACT
            var result = _sut.Index();

            // ASSERT
            _mockViewRepository.Verify(m => m.Get<CityListView>(CityListView.ListViewId));
            ((result as ViewResult).Model as HomeIndexViewModel).Cities.Should().BeEmpty();
        }

        [Fact]
        public void Index_GivenCityListViewWithEmptyCityList_ShouldGetCititesFromTheViewRepository_AndReturnAnEmptyResponseModel()
        {
            // ARRANGE
            _mockViewRepository.Setup(m => m.Get<CityListView>(CityListView.ListViewId)).Returns(new CityListView {Cities = new List<CityListView.City>()});

            // ACT
            var result = _sut.Index();

            // ASSERT
            _mockViewRepository.Verify(m => m.Get<CityListView>(CityListView.ListViewId));
            ((result as ViewResult).Model as HomeIndexViewModel).Cities.Should().BeEmpty();
        }

        [Fact]
        public void Index_GivenTwoCities_ShouldReturnToMappedCitiesInTheResponseModel()
        {
            // ARRANGE
            _mockViewRepository.Setup(m => m.Get<CityListView>(CityListView.ListViewId)).Returns(new CityListView
            {
                Id = CityListView.ListViewId,
                Cities = new List<CityListView.City>
                {
                    new CityListView.City { Name = "Halmstad", Slug = "halmstad"},
                    new CityListView.City { Name = "Borås", Slug = "boras"}
                }
            });

            // ACT
            var result = _sut.Index();

            // ASSERT
            ((result as ViewResult).Model as HomeIndexViewModel).Cities.ShouldBeEquivalentTo(new List<HomeIndexViewModel.CityViewModel>
            {
                new HomeIndexViewModel.CityViewModel { Name = "Halmstad", Slug = "halmstad" },
                new HomeIndexViewModel.CityViewModel { Name = "Borås", Slug = "boras" },
            });
        }
    }
}
