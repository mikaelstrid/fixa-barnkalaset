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
            ViewData["Title"] = "Bloggposter";
            return View(model);
        }

        [Route("skapa")]
        public IActionResult Create()
        {
            _logger.LogDebug("Create GET: Called");
            ViewData["Title"] = "Lägg till ny bloggpost";
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


        [Route("{urlSlug}/andra")]
        public async Task<IActionResult> Edit(string urlSlug)
        {
            _logger.LogDebug("Edit GET: Edit called with slug {Slug}", urlSlug);
            var blogPost = await _blogPostRepository.GetBySlug(urlSlug);
            if (blogPost == null)
            {
                _logger.LogWarning("Edit GET: No blog post with slug {Slug} found when getting blog post", urlSlug);
                return NotFound();
            }

            var model = _mapper.Map<BlogPost, CreateOrEditBlogPostViewModel>(blogPost);
            _logger.LogDebug("Edit GET: Returned model {Model}", JsonConvert.SerializeObject(model));
            ViewData["Title"] = "Ändra bloggpost";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{urlSlug}/andra")]
        public async Task<IActionResult> Edit(string urlSlug, [Bind("Title,Slug,Preamble,Body,IsPublished,PublishedUtc")] CreateOrEditBlogPostViewModel model)
        {
            _logger.LogDebug("Edit POST: Edit called with slug {Slug} and model {Model}", urlSlug, JsonConvert.SerializeObject(model));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Edit POST: Invalid model state {ModelState}", JsonConvert.SerializeObject(ModelState));
                return View(model);
            }

            var existingBlogPost = await _blogPostRepository.GetBySlug(urlSlug);
            if (existingBlogPost == null)
            {
                _logger.LogWarning("Edit POST: No blog post with slug {Slug} found when updating blog post", urlSlug);
                return NotFound();
            }

            if (urlSlug != model.Slug && await _blogPostRepository.GetBySlug(model.Slug) != null)
            {
                _logger.LogWarning("Edit POST: A blog post with slug {Slug} already exists.", model.Slug);
                ModelState.AddModelError("Slug", $"Det finns redan en bloggpost med sluggen {model.Slug}");
                return View(model);
            }

            if (existingBlogPost.Title != model.Title
                || existingBlogPost.Slug != model.Slug
                || existingBlogPost.Body != model.Body
                || existingBlogPost.IsPublished != model.IsPublished
                || existingBlogPost.PublishedUtc != model.PublishedUtc)
            {
                var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                _logger.LogInformation("Edit POST: Edited blog post from {OldBlogPost} to {NewBlogPost}", JsonConvert.SerializeObject(existingBlogPost, settings), JsonConvert.SerializeObject(model, settings));
                existingBlogPost.Title = model.Title;
                existingBlogPost.Slug = model.Slug;
                existingBlogPost.Body = model.Body;
                existingBlogPost.IsPublished = model.IsPublished;
                existingBlogPost.PublishedUtc = model.PublishedUtc;
                await _blogPostRepository.AddOrUpdate(existingBlogPost);
            }
            else
            {
                _logger.LogInformation("Edit POST: No changes detected");
            }

            return RedirectToAction("Index");
        }
    }
}
