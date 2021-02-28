using System;
using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels
{
    public class CreateOrEditBlogPostViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [Display(Name = "Rubrik")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Fältet är obligatoriskt")]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Display(Name = "Brödtext")]
        public string Body { get; set; }

        [Display(Name = "Publicerad")]
        public bool IsPublished { get; set; }

        [Display(Name = "Publiceringsdatum")]
        public DateTime? PublishedUtc { get; set; }
    }
}
