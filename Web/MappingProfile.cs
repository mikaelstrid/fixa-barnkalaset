using AutoMapper;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web.Models;
using Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels;

namespace Pixel.FixaBarnkalaset.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // PUBLIC
            CreateMap<City, HomeIndexViewModel.CityViewModel>();

            CreateMap<Arrangement, ArrangementsIndexViewModel.ArrangementViewModel>();
            CreateMap<Arrangement, ArrangementDetailsViewModel>();

            CreateMap<BlogPost, BlogPostsIndexViewModel.BlogPostViewModel>();
            CreateMap<BlogPost, BlogPostDetailsViewModel>();

            CreateMap<Party, WhereViewModel>();
            CreateMap<Party, WhenViewModel>();


            // ADMIN
            CreateMap<Arrangement, Areas.Admin.ViewModels.ArrangementsIndexViewModel.ArrangementViewModel>();
            CreateMap<Arrangement, Areas.Admin.ViewModels.CreateOrEditArrangementViewModel>().ReverseMap();
            
            CreateMap<City, Areas.Admin.ViewModels.CitiesIndexViewModel.CityViewModel>();
            CreateMap<City, Areas.Admin.ViewModels.CreateOrEditCityViewModel>().ReverseMap();

            CreateMap<BlogPost, Areas.Admin.ViewModels.BlogPostsIndexViewModel.BlogPostViewModel>();
            CreateMap<BlogPost, Areas.Admin.ViewModels.CreateOrEditBlogPostViewModel>().ReverseMap();
        }
    }
}
