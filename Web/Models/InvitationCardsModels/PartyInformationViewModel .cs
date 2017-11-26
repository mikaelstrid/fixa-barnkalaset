﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Models.InvitationCardsModels
{
    public class PartyInformationViewModel : InvitationViewModelBase
    {
        [Required]
        [Display(Name = "Vems kalas är det?")]
        public string NameOfBirthdayChild { get; set; }

        [Required]
        [Display(Name = "Datum")]
        public DateTime? PartyDate { get; set; }

        [Required]
        [Display(Name = "Starttid")]
        public DateTime? PartyStartTime { get; set; }

        [Required]
        [Display(Name = "Sluttid")]
        public DateTime? PartyEndTime { get; set; }

        [Required]
        [Display(Name = "Var ska ni vara?")]
        public string LocationName { get; set; }

        [Required]
        [Display(Name = "Gatuadress")]
        public string StreetAddress { get; set; }

        [Display(Name = "Postnummer")]
        public string PostalCode { get; set; }

        [Display(Name = "Ort")]
        public string PostalCity { get; set; }

        [Display(Name = "O.S.A.-datum")]
        public DateTime? RsvpDate { get; set; }

        [Display(Name = "Beskriv hur ni vill att gästerna ska svara")]
        public string RsvpDescription { get; set; }
    }
}
