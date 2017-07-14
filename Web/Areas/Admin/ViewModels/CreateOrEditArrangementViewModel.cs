using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels
{
    public class CreateOrEditArrangementViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [RegularExpression("^[abcdefghijklmnopqrstuvwxyz0123456789-]*$")]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Display(Name = "Pitch")]
        public string Pitch { get; set; }

        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Display(Name = "Bokningsvillkor")]
        public string BookingConditions { get; set; }

        [Display(Name = "Google Places id")]
        public string GooglePlacesId { get; set; }

        [Display(Name = "Omslagsbild")]
        public string CoverImage { get; set; }

        [Display(Name = "Omslagsbild, tillskrivningar")]
        public string CoverImageAttributions { get; set; }

        [Display(Name = "Gatuadress")]
        public string StreetAddress { get; set; }

        [Display(Name = "Postnummer")]
        [RegularExpression("^\\d\\d\\d ?\\d\\d$", ErrorMessage = "Värdet är inte en giltigt postnummer")]
        public string PostalCode { get; set; }

        [Display(Name = "Postort")]
        public string PostalCity { get; set; }

        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-postadress")]
        [EmailAddress(ErrorMessage = "Värdet är inte en giltig e-postadress")]
        public string EmailAddress { get; set; }

        [Display(Name = "Hemsida")]
        public string Website { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [Range(-90.0, 90.0, ErrorMessage = "Värdet ska vara mellan -90,0 och 90,0")]
        [Display(Name = "Latitud")]
        public double Latitude { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [Range(-180.0, 180.0, ErrorMessage = "Värdet ska vara mellan -180,0 och 180,0")]
        [Display(Name = "Longitud")]
        public double Longitude { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [Display(Name = "Stad")]
        public string CitySlug { get; set; }


        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}
