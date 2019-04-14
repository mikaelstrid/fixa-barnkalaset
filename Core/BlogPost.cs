using System;

namespace Pixel.FixaBarnkalaset.Core
{
    public class BlogPost : Entity<int>
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Body { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedUtc { get; set; }
    }
}
