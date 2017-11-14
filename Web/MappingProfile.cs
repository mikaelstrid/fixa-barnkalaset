using System;
using System.Linq;
using AutoMapper;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Web.ApiControllers;
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
            CreateMap<Party, WhenViewModel>()
                .ForMember(
                    dest => dest.PartyDate,
                    opt => opt.MapFrom(src => src.StartTime.HasValue ? src.StartTime.Value.Date : (DateTime?) null)
                )
                .ForMember(
                    dest => dest.PartyStartTime,
                    opt => opt.MapFrom(src => src.StartTime)
                )
                .ForMember(
                    dest => dest.PartyEndTime,
                    opt => opt.MapFrom(src => src.EndTime)
                );
            CreateMap<Party, WhichViewModel>()
                .ForMember(
                    dest => dest.Invitations,
                    opt => opt.MapFrom(src => src.Invitations.Select(i => new WhichViewModel.InvitationViewModel
                    {
                        Id = i.CompositeId,
                        FirstName = i.Guest.FirstName,
                        LastName = i.Guest.LastName,
                        StreetAddress = i.Guest.StreetAddress,
                        PostalCode = i.Guest.PostalCode,
                        PostalCity = i.Guest.PostalCity
                    }))
                );
            CreateMap<Party, RsvpViewModel>();
            CreateMap<Party, ReviewViewModel>()
                .ForMember(
                    dest => dest.PartyDate,
                    opt => opt.MapFrom(src => src.StartTime.HasValue ? src.StartTime.Value.Date : (DateTime?)null)
                )
                .ForMember(
                    dest => dest.PartyStartTime,
                    opt => opt.MapFrom(src => src.StartTime)
                )
                .ForMember(
                    dest => dest.PartyEndTime,
                    opt => opt.MapFrom(src => src.EndTime)
                );
            CreateMap<Party, ChooseTemplateViewModel>();
            CreateMap<InvitationCardTemplate, ChooseTemplateViewModel.TemplateViewModel>();

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
