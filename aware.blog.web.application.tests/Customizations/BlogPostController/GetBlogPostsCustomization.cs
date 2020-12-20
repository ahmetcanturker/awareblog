using System.Collections.Generic;
using System.Linq;

using AutoFixture;

using Aware.Blog.Contract;
using Aware.Blog.Domain;

using Moq;

namespace Aware.Blog.Web.Application.Tests.BlogPostController
{
    public class GetBlogPostsCustomization : BlogPostControllerCustomization
    {
        public override void Customize(IFixture fixture)
        {
            base.Customize(fixture);

            var blogPosts = fixture.CreateMany<BlogPost>()
                .ToList();

            ApplicationDbContext.Setup(x => x.GetBlogPostsAsync(
                It.IsAny<int>(),
                It.IsAny<int>()))
                .ReturnsAsync(blogPosts);

            ApplicationDbContext.Setup(x => x.GetBlogPostCountAsync())
                .ReturnsAsync(fixture.Create<int>());

            var blogPostDtos = fixture.CreateMany<BlogPostDto>(blogPosts.Count)
                .ToList();

            Mapper.Setup(x => x.Map<IList<BlogPostDto>>(It.IsAny<object>()))
                .Returns(blogPostDtos);
        }
    }
}