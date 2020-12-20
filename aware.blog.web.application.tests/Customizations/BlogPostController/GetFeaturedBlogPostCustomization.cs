using AutoFixture;

using Aware.Blog.Contract;
using Aware.Blog.Domain;

using Moq;

namespace Aware.Blog.Web.Application.Tests.BlogPostController
{
    public class GetFeaturedBlogPostCustomization : BlogPostControllerCustomization
    {
        public override void Customize(IFixture fixture)
        {
            base.Customize(fixture);

            var blogPost = fixture.Create<BlogPost>();

            ApplicationDbContext.Setup(x => x.GetFeaturedBlogPostAsync())
                .ReturnsAsync(blogPost);

            var blogPostDto = fixture.Create<BlogPostDto>();

            Mapper.Setup(x => x.Map<BlogPostDto>(It.IsAny<object>()))
                .Returns(blogPostDto);
        }
    }
}