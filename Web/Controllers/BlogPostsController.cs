using System;
using System.Collections.Generic;
using System.Linq;
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

        //[Route("{citySlug}/{arrangementSlug}")]
        //public async Task<IActionResult> Details(string citySlug, string arrangementSlug)
        //{
        //    var city = await _blogPostRepository.GetBySlug(citySlug);
        //    if (city == null)
        //    {
        //        _logger.LogWarning("Details: No city with slug {CitySlug} found", citySlug);
        //        return NotFound();
        //    }

        //    var arrangement = await _arrangementRepository.GetBySlug(citySlug, arrangementSlug);
        //    if (arrangement == null)
        //    {
        //        _logger.LogWarning("Details: No arrangement with slug {ArrangementSlug} found", arrangementSlug);
        //        return NotFound();
        //    }

        //    ViewData["Title"] = $"Barnkalas på {arrangement.Name}, {city.Name} | Fixa barnkalaset";
        //    ViewData["Description"] = $"{arrangement.Pitch}";

        //    ViewData["OgTitle"] = $"Barnkalas på {arrangement.Name}, {city.Name}";
        //    ViewData["OgDescription"] = $"{arrangement.Pitch}";

        //    var viewModel = _mapper.Map<Arrangement, ArrangementDetailsViewModel>(arrangement);
        //    viewModel.CityName = city.Name;
        //    return View(viewModel);
        //}
    }
}
