using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aware.Blog.Domain
{
    public interface IApplicationDbContext
    {
        #region IBlogPostRepository Members
        Task<int> GetBlogPostCountAsync();
        Task<IList<BlogPost>> GetBlogPostsAsync(int pageIndex, int pageLength);
        Task<BlogPost> GetBlogPostByUriAsync(string uri);
        Task<BlogPost> GetFeaturedBlogPostAsync();
        Task<IList<ArchiveModel>> GetArchivesAsync();
        Task<int> GetArchiveBlogPostCountAsync(int year, int month);
        Task<IList<BlogPost>> GetArchiveBlogPostsAsync(int year, int month, int pageIndex, int pageLength);
        #endregion
    }
}