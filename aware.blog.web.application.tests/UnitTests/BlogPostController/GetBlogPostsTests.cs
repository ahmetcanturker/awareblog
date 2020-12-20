using System.Collections.Generic;
using System.Threading.Tasks;

using Aware.Blog.Contract;
using Aware.Blog.Domain;
using Aware.Blog.Validation;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

using SUT = Aware.Blog.Web.Application.Controllers.BlogPostController;
using UnitCustomization = Aware.Blog.Web.Application.Tests.BlogPostController.GetBlogPostsCustomization;

namespace Aware.Blog.Web.Application.Tests.BlogPostController
{
    [Trait("Tests", "BlogPostController Unit Tests")]
    public class GetBlogPostsTests
    {
        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Validates_Request(
            Mock<IGenericValidator> validator,
            SUT sut,
            int pageIndex,
            int pageLength)
        {
            validator.Setup(x => x.Validate(It.Is<ListRequest>(
                (x) => x.PageIndex == pageIndex && x.PageLength == pageLength)));

            await sut.GetBlogPosts(pageIndex, pageLength);

            validator.Verify(x => x.Validate(It.Is<ListRequest>(
                (x) => x.PageIndex == pageIndex && x.PageLength == pageLength)),
                Times.Once());
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Ok_Result(
            SUT sut,
            int pageIndex,
            int pageLength)
        {
            var result = await sut.GetBlogPosts(pageIndex, pageLength);

            result.Should()
                .BeOfType<OkObjectResult>();

            (result as OkObjectResult).Value.Should()
                .NotBeNull();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Has_Result_Value(
            SUT sut,
            int pageIndex,
            int pageLength)
        {
            var result = await sut.GetBlogPosts(pageIndex, pageLength);

            (result as OkObjectResult).Value.Should()
                .NotBeNull();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Successful(
            SUT sut,
            int pageIndex,
            int pageLength)
        {
            var result = await sut.GetBlogPosts(pageIndex, pageLength) as OkObjectResult;

            var response = result.Value as Response;

            response.Success
                .Should()
                .BeTrue();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Gets_BlogPosts(
            Mock<IApplicationDbContext> applicationDbContext,
            SUT sut,
            int pageIndex,
            int pageLength,
            IList<BlogPost> blogPosts)
        {
            applicationDbContext.Setup(x => x.GetBlogPostsAsync(
                ItIs.Equal(pageIndex),
                ItIs.Equal(pageLength)))
                .ReturnsAsync(blogPosts);

            var result = await sut.GetBlogPosts(pageIndex, pageLength) as OkObjectResult;

            var response = result.Value as PaginatedListResponse<BlogPostDto>;

            response.Data.Count
                .Should()
                .Be(blogPosts.Count);
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Gets_TotalCount(
            Mock<IApplicationDbContext> applicationDbContext,
            SUT sut,
            int pageIndex,
            int pageLength,
            int totalCount)
        {
            applicationDbContext.Setup(x => x.GetBlogPostCountAsync())
                .ReturnsAsync(totalCount);

            var result = await sut.GetBlogPosts(pageIndex, pageLength) as OkObjectResult;

            var response = result.Value as PaginatedListResponse<BlogPostDto>;

            response.TotalCount
                .Should()
                .Be(totalCount);
        }
    }
}