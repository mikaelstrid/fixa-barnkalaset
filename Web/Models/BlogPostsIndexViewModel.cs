using System;
using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Web.Models
{
    public class BlogPostsIndexViewModel
    {
        public IEnumerable<BlogPostViewModel> BlogPosts { get; set; }

        public class BlogPostViewModel
        {
            public string Title { get; set; }
            public string Slug { get; set; }
            public string Body { get; set; }
            public DateTime? PublishedUtc { get; set; }
        }
    }
}