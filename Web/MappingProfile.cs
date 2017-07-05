using AutoMapper;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web.Models;

namespace Pixel.FixaBarnkalaset.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // PUBLIC
            CreateMap<City, HomeIndexViewModel.CityViewModel>();
            CreateMap<Arrangement, ArrangementIndexViewModel.ArrangementViewModel>();
            CreateMap<Arrangement, ArrangementDetailsViewModel>();


            // ADMIN
            CreateMap<Arrangement, Areas.Admin.ViewModels.CreateOrEditArrangementViewModel>();
            CreateMap<Arrangement, Areas.Admin.ViewModels.ArrangementsIndexViewModel.ArrangementViewModel>();

            CreateMap<City, Areas.Admin.ViewModels.CitiesIndexViewModel.CityViewModel>();
            CreateMap<City, Areas.Admin.ViewModels.CitiesIndexViewModel>();
            CreateMap<City, Areas.Admin.ViewModels.CreateOrEditCityViewModel>();
            CreateMap<Areas.Admin.ViewModels.CreateOrEditCityViewModel, City>();
            CreateMap<Areas.Admin.ViewModels.CreateOrEditArrangementViewModel, Arrangement>();
        }
    }
}
