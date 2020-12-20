using System.Net;
using System.Threading.Tasks;

using Aware.Blog.Contract;
using Aware.Blog.Domain;
using Aware.Blog.Validation;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

using SUT = Aware.Blog.Web.Application.Controllers.BlogPostController;
using UnitCustomization = Aware.Blog.Web.Application.Tests.BlogPostController.GetFeaturedBlogPostCustomization;

namespace Aware.Blog.Web.Application.Tests.BlogPostController
{
    [Trait("Tests", "BlogPostController Unit Tests")]
    public class GetFeaturedBlogPostTests
    {
        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Ok_Result(
            SUT sut)
        {
            var result = await sut.GetFeatureBlogPost();

            result.Should()
                .BeOfType<OkObjectResult>();

            (result as OkObjectResult).Value.Should()
                .NotBeNull();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Has_Result_Value(
            SUT sut)
        {
            var result = await sut.GetFeatureBlogPost();

            (result as OkObjectResult).Value.Should()
                .NotBeNull();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Successful(
            SUT sut)
        {
            var result = await sut.GetFeatureBlogPost() as OkObjectResult;

            var response = result.Value as Response;

            response.Success
                .Should()
                .BeTrue();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Error_When_BlogPost_Is_Null(
            Mock<IApplicationDbContext> applicationDbContext,
            SUT sut)
        {
            applicationDbContext.Setup(x => x.GetFeaturedBlogPostAsync())
                .ReturnsAsync((BlogPost)null);

            var result = await sut.GetFeatureBlogPost();

            result.Should()
                .BeOfType<ObjectResult>();

            (result as ObjectResult).StatusCode
                .Should()
                .Be((int)HttpStatusCode.NotFound);

            var response = (result as ObjectResult).Value as Response;

            response.StatusCode
                .Should()
                .Be((int)HttpStatusCode.NotFound);
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Gets_Featured_BlogPost(
            Mock<IApplicationDbContext> applicationDbContext,
            SUT sut,
            BlogPost blogPost)
        {
            applicationDbContext.Setup(x => x.GetFeaturedBlogPostAsync())
                .ReturnsAsync(blogPost);

            var result = await sut.GetFeatureBlogPost() as OkObjectResult;

            var response = result.Value as DataResponse<BlogPostDto>;

            response.Data
                .Should()
                .NotBeNull();
        }
    }
}