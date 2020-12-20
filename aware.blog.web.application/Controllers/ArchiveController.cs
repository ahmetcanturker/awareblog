using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Aware.Blog.Contract;
using Aware.Blog.Domain;
using Aware.Blog.Validation;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aware.Blog.Web.Application.Controllers
{
    [Route("api/archives")]
    public class ArchiveController : ApplicationController<ArchiveController>
    {
        public ArchiveController(
            ILogger<ArchiveController> logger,
            IApplicationDbContext applicationDbContext,
            IGenericValidator genericValidator,
            IMapper mapper) : base(logger, applicationDbContext, genericValidator, mapper)
        { }

        [HttpGet]
        public async Task<IActionResult> GetArchives()
        {
            var archives = await ApplicationDbContext.GetArchivesAsync();

            var archiveDtos = Map<IList<ArchiveDto>>(archives);

            return List(archiveDtos);
        }

        [HttpGet("{year}/{month}")]
        public async Task<IActionResult> GetBlogPosts(
            [FromRoute] int year,
            [FromRoute] int month,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageLength = 50)
        {
            Validate(new GetArchiveBlogPostsRequest
            {
                Year = year,
                Month = month,
                PageIndex = pageIndex,
                PageLength = pageLength
            });

            var totalCount = await ApplicationDbContext.GetArchiveBlogPostCountAsync(year, month);
            var blogPosts = await ApplicationDbContext.GetArchiveBlogPostsAsync(year, month, pageIndex, pageLength);

            var blogPostDtos = Map<IList<BlogPostDto>>(blogPosts);

            return PaginatedList(blogPostDtos, totalCount);
        }
    }
}