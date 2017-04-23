using AutoMapper;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Web.Areas.Admin.ViewModels;

namespace Pixel.Kidsparties.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<City, CityViewModel>();
            //CreateMap<CityViewModel, City>();
            CreateMap<Arrangement, CreateOrEditArrangementViewModel>();
            CreateMap<CreateOrEditArrangementViewModel, Arrangement>();
        }
    }
}
