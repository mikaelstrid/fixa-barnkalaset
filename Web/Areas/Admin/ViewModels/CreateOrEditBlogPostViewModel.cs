using System;
using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels
{
    public class CreateOrEditBlogPostViewModel
    {
        [Display(Name = "Rubrik")]
        public string Title { get; set; }

        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Display(Name = "Ingress")]
        public string Preamble { get; set; }

        [Display(Name = "Brödtext")]
        public string Body { get; set; }

        [Display(Name = "Publicerad")]
        public bool IsPublished { get; set; }

        [Display(Name = "Publiceringsdatum")]
        public DateTime PublishedUtc { get; set; }
    }
}
