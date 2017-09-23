using System;
using Pixel.FixaBarnkalaset.Core;

namespace UnitTests.Utilities.TestDataExtensions
{
    public static class BlogPostTestDataExtensions
    {
        public static BlogPost With(this BlogPost blogPost, Action<BlogPost> action)
        {
            action(blogPost);
            return blogPost;
        }

        public static BlogPost PubliceradFemteSeptember(this BlogPost blogPost)
        {
            blogPost.Title = "De bästa barnkalasen i Halmstad 2017";
            blogPost.Slug = "de-basta-barnkalasen-i-halmstad-2017";
            blogPost.Preamble = "Vi har listat våra favoriter bland barnkalasen i Halmstad.";
            blogPost.Body = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam tempus porta urna, ac lobortis dui pharetra id. Nam massa nulla, cursus vitae leo id, pulvinar mattis tortor. Vivamus eu elit ante. Integer lacinia felis magna, id sollicitudin mauris vestibulum eu. Nam sit amet dapibus diam, eget aliquet sem. In erat ante, vulputate nec fringilla vel, tempus et enim. In a vulputate dolor. Praesent laoreet, dolor faucibus gravida auctor, nunc est placerat justo, quis egestas diam est quis urna. Fusce non nulla dui. Ut imperdiet sapien ut tempus viverra. Etiam fringilla aliquet nunc, quis tempor velit elementum at.</p><p>Sed volutpat placerat nibh, non auctor nunc vulputate vel. Morbi egestas urna et lorem mattis, ac feugiat felis tincidunt. Nulla blandit porttitor augue sit amet porta. Nunc tincidunt odio id est ultricies sollicitudin. Aliquam auctor urna eu turpis malesuada, a tincidunt sapien efficitur. Fusce eget fringilla lacus. Morbi sollicitudin, massa quis faucibus consequat, orci magna venenatis diam, et rhoncus mauris risus in tortor. Cras vitae nibh dui. Nulla facilisi. Vivamus dignissim, neque a bibendum iaculis, neque mauris ornare nisl, id tristique tellus felis ut urna. Donec vulputate euismod dignissim. Suspendisse pellentesque pharetra consectetur. Praesent vehicula suscipit scelerisque. Donec quis mauris ut lacus condimentum ultrices sed vel mi. Etiam nec dui laoreet, elementum erat eu, auctor nisi. Pellentesque commodo, lectus feugiat rhoncus vestibulum, nisl magna egestas ipsum, id feugiat dui ex vel erat.</p>";
            blogPost.IsPublished = true;
            blogPost.PublishedUtc = DateTime.Parse("2017-09-05 13:17");
            return blogPost;
        }

        public static BlogPost PubliceradSjuttondeOktober(this BlogPost blogPost)
        {
            blogPost.Title = "De bästa barnkalasen i Malmö 2017";
            blogPost.Slug = "de-basta-barnkalasen-i-malmo-2017";
            blogPost.Preamble = "Vi har listat våra favoriter bland barnkalasen i Malmö.";
            blogPost.Body = "<p>Praesent mollis sapien at dui condimentum suscipit. Morbi ultricies urna sit amet mauris mollis suscipit. Vestibulum in sodales arcu. Nullam quis orci ut sem sodales ultrices. Nullam risus purus, euismod id hendrerit vitae, rutrum non lacus. Pellentesque lectus orci, dignissim et nulla id, finibus consectetur odio. Suspendisse eget molestie tellus, mollis facilisis velit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse potenti. Nulla pharetra, nunc vel gravida luctus, urna magna tincidunt risus, vel auctor augue arcu ac dui. Nunc accumsan diam sed ipsum lacinia, quis interdum nibh rhoncus.</p>";
            blogPost.IsPublished = true;
            blogPost.PublishedUtc = DateTime.Parse("2017-10-17 09:12");
            return blogPost;
        }

        public static BlogPost EjPubliceradIngetPubliceringsDatum(this BlogPost blogPost)
        {
            blogPost.Title = "De bästa barnkalasen i Malmö 2017";
            blogPost.Slug = "de-basta-barnkalasen-i-malmo-2017";
            blogPost.Preamble = "Vi har listat våra favoriter bland barnkalasen i Malmö.";
            blogPost.Body = "<p>Praesent mollis sapien at dui condimentum suscipit. Morbi ultricies urna sit amet mauris mollis suscipit. Vestibulum in sodales arcu. Nullam quis orci ut sem sodales ultrices. Nullam risus purus, euismod id hendrerit vitae, rutrum non lacus. Pellentesque lectus orci, dignissim et nulla id, finibus consectetur odio. Suspendisse eget molestie tellus, mollis facilisis velit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse potenti. Nulla pharetra, nunc vel gravida luctus, urna magna tincidunt risus, vel auctor augue arcu ac dui. Nunc accumsan diam sed ipsum lacinia, quis interdum nibh rhoncus.</p>";
            blogPost.IsPublished = false;
            blogPost.PublishedUtc = null;
            return blogPost;
        }

        public static BlogPost EjPubliceradPubliceringsDatumElfteNovember(this BlogPost blogPost)
        {
            blogPost.Title = "De bästa barnkalasen i Malmö 2017";
            blogPost.Slug = "de-basta-barnkalasen-i-malmo-2017";
            blogPost.Preamble = "Vi har listat våra favoriter bland barnkalasen i Malmö.";
            blogPost.Body = "<p>Praesent mollis sapien at dui condimentum suscipit. Morbi ultricies urna sit amet mauris mollis suscipit. Vestibulum in sodales arcu. Nullam quis orci ut sem sodales ultrices. Nullam risus purus, euismod id hendrerit vitae, rutrum non lacus. Pellentesque lectus orci, dignissim et nulla id, finibus consectetur odio. Suspendisse eget molestie tellus, mollis facilisis velit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Suspendisse potenti. Nulla pharetra, nunc vel gravida luctus, urna magna tincidunt risus, vel auctor augue arcu ac dui. Nunc accumsan diam sed ipsum lacinia, quis interdum nibh rhoncus.</p>";
            blogPost.IsPublished = false;
            blogPost.PublishedUtc = DateTime.Parse("2017-11-11 09:12"); 
            return blogPost;
        }
    }
}
