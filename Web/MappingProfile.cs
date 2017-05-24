using AutoMapper;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;
using Pixel.FixaBarnkalaset.Web.Models;

namespace Pixel.FixaBarnkalaset.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<City, CityViewModel>();
            //CreateMap<CityViewModel, City>();
            CreateMap<Arrangement, CreateOrEditArrangementViewModel>();
            CreateMap<CreateOrEditArrangementViewModel, Arrangement>();

            CreateMap<City, HomeIndexViewModel.CityViewModel>();
            CreateMap<Arrangement, ArrangementIndexViewModel.ArrangementViewModel>();
            CreateMap<Arrangement, ArrangementDetailsViewModel>();
        }
    }
}
