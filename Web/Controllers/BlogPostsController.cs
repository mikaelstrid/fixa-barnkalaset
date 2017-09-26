using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Models;

namespace Pixel.FixaBarnkalaset.Web.Controllers
{
    [Route("blogg")]
    public class BlogPostsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<BlogPostsController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostsController(IMapper mapper, ILogger<BlogPostsController> logger, IBlogPostRepository blogPostRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _blogPostRepository = blogPostRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var title = "Barnkalasbloggen";
            var description = "Vi på fixabarnkalaset.se samlar våra bästa tips och trix angående barnkalas i vår blogg.";

            ViewData["Title"] = $"{title} | Fixa barnkalaset";
            ViewData["Description"] = description;

            ViewData["OgTitle"] = title;
            ViewData["OgDescription"] = description;
            ViewData["OgImage"] = Request?.Scheme + "://" + Request?.Host + "/images/balloons-1869790_1200_630.jpg";

            var blogPosts = await _blogPostRepository.GetAll() ?? new BlogPost[0];
            return View(new BlogPostsIndexViewModel
            {
                BlogPosts = _mapper.Map<IEnumerable<BlogPost>, IEnumerable<BlogPostsIndexViewModel.BlogPostViewModel>>(
                    blogPosts
                        .Where(p => p.IsPublished && p.PublishedUtc < DateTime.UtcNow)
                        .OrderByDescending(p => p.PublishedUtc))
            });
        }

        [Route("{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            var blogPost = await _blogPostRepository.GetBySlug(slug);
            if (blogPost == null)
            {
                _logger.LogWarning("Details: No blog post with slug {Slug} found", slug);
                return NotFound();
            }

            if (!blogPost.IsPublished || blogPost.PublishedUtc > DateTime.UtcNow)
            {
                _logger.LogWarning("Details: Blog post with slug {Slug} is not published yet", slug);
                return NotFound();
            }

            ViewData["Title"] = $"{blogPost.Title} | Fixa barnkalaset";
            ViewData["Description"] = LimitLength(blogPost.Body, 160, true);

            ViewData["OgTitle"] = blogPost.Title;
            ViewData["OgDescription"] = LimitLength(blogPost.Body, 300, true);
            ViewData["OgImage"] = Request?.Scheme + "://" + Request?.Host + "/images/balloons-1869790_1200_630.jpg";

            var viewModel = _mapper.Map<BlogPost, BlogPostDetailsViewModel>(blogPost);
            return View(viewModel);
        }


        private static string LimitLength(string inputString, int length, bool appendEllipsis)
        {
            var strippedInput = StripHtml(inputString);
            if (appendEllipsis)
            {
                return strippedInput.Length <= length ? strippedInput : $"{strippedInput.Substring(0, length - 3)}...";
            }
            else
            {
                return strippedInput.Length <= length ? strippedInput : strippedInput.Substring(0, length);
            }
        }

        private static string StripHtml(string inputString)
        {
            return Regex.Replace(inputString, "<.*?>", "");
        }

    }
}
