using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Aware.Blog.Domain.EF.Configurations;

using Microsoft.EntityFrameworkCore;

namespace Aware.Blog.Domain.EF
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BlogPostConfiguration());
            modelBuilder.ApplyConfiguration(new BlogPostTagConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        #region IBlogPostRepository Members
        public async Task<int> GetBlogPostCountAsync() => await BlogPosts.CountAsync();
        public async Task<IList<BlogPost>> GetBlogPostsAsync(int pageIndex, int pageLength)
        {
            // TODO: Projection
            return await BlogPosts
                .Include(x => x.Image)
                .Include(x => x.Author)
                .OrderByDescending(x => x.CreatedTime)
                .Skip(pageIndex * pageLength)
                .Take(pageLength)
                .ToListAsync();
        }

        public async Task<BlogPost> GetBlogPostByUriAsync(string uri)
        {
            // TODO: Projection
            return await BlogPosts
                .Include(x => x.Image)
                .Include(x => x.Author)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Where(x => x.Uri == uri)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<ArchiveModel>> GetArchivesAsync()
        {
            return await BlogPosts
                .GroupBy(x => new
                {
                    x.CreatedTime.Year,
                    x.CreatedTime.Month
                })
                .Select(x => new ArchiveModel
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .ToListAsync();
        }

        public async Task<BlogPost> GetFeaturedBlogPostAsync()
        {
            // TODO: Projection
            return await BlogPosts
                .Include(x => x.Image)
                .Include(x => x.Author)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .OrderByDescending(x => x.CreatedTime)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetArchiveBlogPostCountAsync(int year, int month) => await BlogPosts
            .Where(x => x.CreatedTime.Year == year && x.CreatedTime.Month == month)
            .CountAsync();
        public async Task<IList<BlogPost>> GetArchiveBlogPostsAsync(int year, int month, int pageIndex, int pageLength)
        {
            // TODO: Projection
            return await BlogPosts
                .Where(x => x.CreatedTime.Year == year && x.CreatedTime.Month == month)
                .Include(x => x.Image)
                .Include(x => x.Author)
                .OrderByDescending(x => x.CreatedTime)
                .Skip(pageIndex * pageLength)
                .Take(pageLength)
                .ToListAsync();
        }
        #endregion
    }
}