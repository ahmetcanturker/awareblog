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
using UnitCustomization = Aware.Blog.Web.Application.Tests.BlogPostController.GetBlogPostCustomization;

namespace Aware.Blog.Web.Application.Tests.BlogPostController
{
    [Trait("Tests", "BlogPostController Unit Tests")]
    public class GetBlogPostTests
    {
        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Validates_Request(
            Mock<IGenericValidator> validator,
            SUT sut,
            string uri)
        {
            validator.Setup(x => x.Validate(It.Is<GetBlogPostByUriRequest>(
                (x) => x.Uri == uri)));

            await sut.GetBlogPost(uri);

            validator.Verify(x => x.Validate(It.Is<GetBlogPostByUriRequest>(
                (x) => x.Uri == uri)),
                Times.Once());
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Ok_Result(
            SUT sut,
            string uri)
        {
            var result = await sut.GetBlogPost(uri);

            result.Should()
                .BeOfType<OkObjectResult>();

            (result as OkObjectResult).Value.Should()
                .NotBeNull();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Has_Result_Value(
            SUT sut,
            string uri)
        {
            var result = await sut.GetBlogPost(uri);

            (result as OkObjectResult).Value.Should()
                .NotBeNull();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Successful(
            SUT sut,
            string uri)
        {
            var result = await sut.GetBlogPost(uri) as OkObjectResult;

            var response = result.Value as Response;

            response.Success
                .Should()
                .BeTrue();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Error_When_BlogPost_Is_Null(
            Mock<IApplicationDbContext> applicationDbContext,
            SUT sut,
            string uri)
        {
            applicationDbContext.Setup(x => x.GetBlogPostByUriAsync(
                It.IsAny<string>()))
                .ReturnsAsync((BlogPost)null);

            var result = await sut.GetBlogPost(uri);

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
        public async Task Gets_BlogPost(
            Mock<IApplicationDbContext> applicationDbContext,
            SUT sut,
            string uri,
            BlogPost blogPost)
        {
            applicationDbContext.Setup(x => x.GetBlogPostByUriAsync(
                ItIs.Equal(uri)))
                .ReturnsAsync(blogPost);

            var result = await sut.GetBlogPost(uri) as OkObjectResult;

            var response = result.Value as DataResponse<BlogPostDto>;

            response.Data
                .Should()
                .NotBeNull();
        }
    }
}