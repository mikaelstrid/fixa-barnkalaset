using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pixel.FixaBarnkalaset.Core;
using Pixel.FixaBarnkalaset.Core.Interfaces;
using Pixel.FixaBarnkalaset.Infrastructure.Persistence.EntityFramework;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public class SqlBlogPostRepository : IBlogPostRepository
    {
        private readonly MyDataDbContext _dbContext;
        private readonly ILogger _logger;

        public SqlBlogPostRepository(MyDataDbContext dbContext, ILogger<SqlCityRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<BlogPost>> GetAll()
        {
            _logger.LogDebug("GetAll: Get all blog posts");
            return await _dbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetById(int id)
        {
            _logger.LogDebug("GetById: Get blog post with id {Id}", id);
            return await _dbContext.BlogPosts.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<BlogPost> GetBySlug(string slug)
        {
            _logger.LogDebug("GetBySlug: Get blog post with slug {Slug}", slug);
            return await _dbContext.BlogPosts.SingleOrDefaultAsync(c => c.Slug == slug);
        }

        public async Task AddOrUpdate(BlogPost blogPost)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            _logger.LogDebug("AddOrUpdate: Adding or updating blog post with id {Id} with data {BlogPost}", blogPost.Id, JsonConvert.SerializeObject(blogPost, settings));
            var existingBlogPost = await GetById(blogPost.Id);
            if (existingBlogPost != null)
            {
                existingBlogPost.Title = blogPost.Title;
                existingBlogPost.Slug = blogPost.Slug;
                existingBlogPost.Body = blogPost.Body;
                existingBlogPost.IsPublished = blogPost.IsPublished;
                existingBlogPost.PublishedUtc = blogPost.PublishedUtc;
                _dbContext.Update(existingBlogPost);
                _logger.LogInformation("AddOrUpdate: Updated blog post with id {Id} with data {BlogPost}", blogPost.Id, JsonConvert.SerializeObject(blogPost, settings));
            }
            else
            {
                await _dbContext.BlogPosts.AddAsync(blogPost);
                _logger.LogInformation("AddOrUpdate: Added blog post with id {Id} with data {BlogPost}", blogPost.Id, JsonConvert.SerializeObject(blogPost, settings));
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            _logger.LogDebug("Remove: Removing blog post with id {Id}", id);
            var blogPost = await GetById(id);
            if (blogPost == null)
            {
                _logger.LogInformation("Remove: Blog post with id {Id} not found, continue", id);
                return;
            }
            _dbContext.BlogPosts.Remove(blogPost);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Remove: Blog post with id {Id} removed", id);
        }
    }
}
