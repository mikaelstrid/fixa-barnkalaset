using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Web.Areas.Admin.ViewModels;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core;

namespace Pixel.FixaBarnkalaset.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/bloggposter")]
    [Authorize(Roles = "Admin")]
    public class BlogPostsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostsController(
            IMapper mapper,
            ILogger<BlogPostsController> logger,
            IBlogPostRepository blogPostRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _blogPostRepository = blogPostRepository;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            _logger.LogDebug("Index: Called");
            var blogPosts = await _blogPostRepository.GetAll() ?? new List<BlogPost>();
            var model = new BlogPostsIndexViewModel
            {
                BlogPosts = _mapper.Map<IEnumerable<BlogPost>, IEnumerable<BlogPostsIndexViewModel.BlogPostViewModel>>(blogPosts)
            };
            ViewData["Title"] = "Bloggposter | Fixa barnkalaset";
            return View(model);
        }

        [Route("skapa")]
        public IActionResult Create()
        {
            _logger.LogDebug("Create GET: Called");
            ViewData["Title"] = "Lägg till ny bloggpost | Fixa barnkalaset";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("skapa")]
        public async Task<IActionResult> Create([Bind("Title,Slug,Preamble,Body,IsPublished,PublishedUtc")] CreateOrEditBlogPostViewModel model)
        {
            _logger.LogDebug("Create POST: called");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                return View(model);
            }

            if (await _blogPostRepository.GetBySlug(model.Slug) != null)
            {
                _logger.LogWarning("Create POST: There already exist a blog post with slug {Slug}", model.Slug);
                ModelState.AddModelError("Slug", $"Det finns redan en bloggpost med sluggen {model.Slug}");
                return View(model);
            }

            var blogPost = _mapper.Map<CreateOrEditBlogPostViewModel, BlogPost>(model);
            await _blogPostRepository.AddOrUpdate(blogPost);
            _logger.LogInformation("Create POST: Created blog post {BlogPost} with slug {Slug}", JsonConvert.SerializeObject(blogPost), model.Slug);
            return RedirectToAction("Index");
        }


        //[Route("{urlSlug}/andra")]
        //public async Task<IActionResult> Edit(string urlSlug)
        //{
        //    _logger.LogDebug("Edit GET: Edit called with slug {Slug}", urlSlug);
        //    var city = await _blogPostRepository.GetBySlug(urlSlug);
        //    if (city == null)
        //    {
        //        _logger.LogWarning("Edit GET: No city with slug {Slug} found when getting city", urlSlug);
        //        return NotFound();
        //    }

        //    var model = _mapper.Map<City, CreateOrEditCityViewModel>(city);
        //    _logger.LogDebug("Edit GET: Returned model {Model}", JsonConvert.SerializeObject(model));
        //    ViewData["Title"] = $"Ändra {city.Name} | Fixa barnkalaset";
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("{urlSlug}/andra")]
        //public async Task<IActionResult> Edit(string urlSlug, [Bind("Name,Slug,Latitude,Longitude")] CreateOrEditCityViewModel model)
        //{
        //    _logger.LogDebug("Edit POST: Edit called with slug {Slug} and model {Model}", urlSlug, JsonConvert.SerializeObject(model));

        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogWarning("Edit POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
        //        return View(model);
        //    }

        //    var existingCity = await _blogPostRepository.GetBySlug(urlSlug);
        //    if (existingCity == null)
        //    {
        //        _logger.LogWarning("Edit POST: No city with slug {Slug} found when updating city", urlSlug);
        //        return NotFound();
        //    }

        //    if (urlSlug != model.Slug && await _blogPostRepository.GetBySlug(model.Slug) != null)
        //    {
        //        _logger.LogWarning("Edit POST: A city with slug {Slug} already exists.", model.Slug);
        //        ModelState.AddModelError("Slug", $"Det finns redan en stad med sluggen {model.Slug}");
        //        return View(model);
        //    }

        //    if (existingCity.Name != model.Name 
        //        || existingCity.Slug != model.Slug 
        //        || existingCity.Latitude != model.Latitude 
        //        || existingCity.Longitude != model.Longitude)
        //    {
        //        var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        //        _logger.LogInformation("Edit POST: Edited city from {OldCity} to {NewCity}", JsonConvert.SerializeObject(existingCity, settings), JsonConvert.SerializeObject(model, settings));
        //        existingCity.Name = model.Name;
        //        existingCity.Slug = model.Slug;
        //        existingCity.Latitude = model.Latitude;
        //        existingCity.Longitude = model.Longitude;
        //        await _blogPostRepository.AddOrUpdate(existingCity);
        //    }
        //    else
        //    {
        //        _logger.LogInformation("Edit POST: No changes detected");
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}
