using System;
using System.Linq;
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
            
            CreateMap<Party, ChooseTemplateViewModel>();
            CreateMap<InvitationCardTemplate, ChooseTemplateViewModel.TemplateViewModel>();
            CreateMap<Party, PartyInformationViewModel>()
                .ForMember(dest => dest.PartyId, opt => opt.MapFrom(src => src.Id))
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
            CreateMap<Invitation, GuestsViewModel.InvitationViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Guest.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Guest.LastName))
                .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.Guest.StreetAddress))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Guest.PostalCode))
                .ForMember(dest => dest.PostalCity, opt => opt.MapFrom(src => src.Guest.PostalCity));
            CreateMap<Party, GuestsViewModel>()
                .ForMember(dest => dest.PartyId, opt => opt.MapFrom(src => src.Id));
                //.ForMember(
                //    dest => dest.Invitations,
                //    opt => opt.MapFrom(src => src.Invitations.Select(i => new GuestsViewModel.InvitationViewModel
                //    {
                //        Id = i.CompositeId,
                //        FirstName = i.Guest.FirstName,
                //        LastName = i.Guest.LastName,
                //        StreetAddress = i.Guest.StreetAddress,
                //        PostalCode = i.Guest.PostalCode,
                //        PostalCity = i.Guest.PostalCity
                //    }))
                //);


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
