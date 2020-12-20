using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using AutoMapper;

using Aware.Blog.Contract;
using Aware.Blog.Domain;
using Aware.Blog.Validation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aware.Blog.Web.Application.Controllers
{
    [Route("api/blog-posts")]
    public class BlogPostController : ApplicationController<BlogPostController>
    {
        public BlogPostController(
            ILogger<BlogPostController> logger,
            IApplicationDbContext applicationDbContext,
            IGenericValidator genericValidator,
            IMapper mapper) : base(logger, applicationDbContext, genericValidator, mapper)
        { }

        [HttpGet]
        public async Task<IActionResult> GetBlogPosts(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageLength = 50)
        {
            Validate(new ListRequest
            {
                PageIndex = pageIndex,
                PageLength = pageLength
            });

            var totalCount = await ApplicationDbContext.GetBlogPostCountAsync();
            var blogPosts = await ApplicationDbContext.GetBlogPostsAsync(pageIndex, pageLength);

            var blogPostDtos = Map<IList<BlogPostDto>>(blogPosts);

            return PaginatedList(blogPostDtos, totalCount);
        }

        [HttpGet("{blogPostUri}")]
        public async Task<IActionResult> GetBlogPost(
            [FromRoute] string blogPostUri)
        {
            var uri = WebUtility.UrlDecode(blogPostUri);

            Validate(new GetBlogPostByUriRequest
            {
                Uri = uri
            });

            var blogPost = await ApplicationDbContext.GetBlogPostByUriAsync(uri);

            if (blogPost == null)
                return Error(new Error
                {
                    ErrorCode = "NotFound",
                    ErrorMessage = "Blog post not found",
                    StatusCode = (int)HttpStatusCode.NotFound
                });

            var blogPostDto = Map<BlogPostDto>(blogPost);

            return Data(blogPostDto);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeatureBlogPost()
        {
            var blogPost = await ApplicationDbContext.GetFeaturedBlogPostAsync();

            if (blogPost == null)
                return Error(new Error
                {
                    ErrorCode = "NotFound",
                    ErrorMessage = "Blog post not found",
                    StatusCode = (int)HttpStatusCode.NotFound
                });

            var blogPostDto = Map<BlogPostDto>(blogPost);

            return Data(blogPostDto);
        }
    }
}