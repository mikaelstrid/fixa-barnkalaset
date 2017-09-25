using System;

namespace Pixel.FixaBarnkalaset.Web.Models
{
    public class BlogPostDetailsViewModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public DateTime? PublishedUtc { get; set; }
    }
}