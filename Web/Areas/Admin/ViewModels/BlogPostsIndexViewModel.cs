using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels
{
    public class BlogPostsIndexViewModel
    {
        [Display(Name = "Bloggposter")]
        public IEnumerable<BlogPostViewModel> BlogPosts { get; set; }

        public class BlogPostViewModel
        {
            [Display(Name = "Rubrik")]
            public string Title { get; set; }

            [Display(Name = "Slug")]
            public string Slug { get; set; }

            [Display(Name = "Publicerad")]
            public bool IsPublished { get; set; }

            [Display(Name = "Publiceringsdatum")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime? PublishedUtc { get; set; }
        }
    }
}
