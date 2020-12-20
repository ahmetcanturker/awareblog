using System.Collections.Generic;
using System.Threading.Tasks;

using Aware.Blog.Contract;
using Aware.Blog.Domain;
using Aware.Blog.Validation;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

using SUT = Aware.Blog.Web.Application.Controllers.ArchiveController;
using UnitCustomization = Aware.Blog.Web.Application.Tests.ArchiveController.GetBlogPostsCustomization;

namespace Aware.Blog.Web.Application.Tests.ArchiveController
{
    [Trait("Tests", "ArchiveController Unit Tests")]
    public class GetBlogPostsTests
    {
        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Validates_Request(
            Mock<IGenericValidator> validator,
            SUT sut,
            int year,
            int month,
            int pageIndex,
            int pageLength)
        {
            validator.Setup(x => x.Validate(It.Is<GetArchiveBlogPostsRequest>(
                (x) => x.Year == year && x.Month == month && x.PageIndex == pageIndex && x.PageLength == pageLength)));

            await sut.GetBlogPosts(year, month, pageIndex, pageLength);

            validator.Verify(x => x.Validate(It.Is<GetArchiveBlogPostsRequest>(
                (x) => x.Year == year && x.Month == month && x.PageIndex == pageIndex && x.PageLength == pageLength)),
                Times.Once());
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Ok_Result(
            SUT sut,
            int year,
            int month,
            int pageIndex,
            int pageLength)
        {
            var result = await sut.GetBlogPosts(year, month, pageIndex, pageLength);

            result.Should()
                .BeOfType<OkObjectResult>();

            (result as OkObjectResult).Value.Should()
                .NotBeNull();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Has_Result_Value(
            SUT sut,
            int year,
            int month,
            int pageIndex,
            int pageLength)
        {
            var result = await sut.GetBlogPosts(year, month, pageIndex, pageLength);

            (result as OkObjectResult).Value.Should()
                .NotBeNull();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Successful(
            SUT sut,
            int year,
            int month,
            int pageIndex,
            int pageLength)
        {
            var result = await sut.GetBlogPosts(year, month, pageIndex, pageLength) as OkObjectResult;

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
            int year,
            int month,
            int pageIndex,
            int pageLength,
            IList<BlogPost> blogPosts)
        {
            applicationDbContext.Setup(x => x.GetArchiveBlogPostsAsync(
                ItIs.Equal(year), 
                ItIs.Equal(month), 
                ItIs.Equal(pageIndex), 
                ItIs.Equal(pageLength)))
                .ReturnsAsync(blogPosts);

            var result = await sut.GetBlogPosts(year, month, pageIndex, pageLength) as OkObjectResult;

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
            int year,
            int month,
            int pageIndex,
            int pageLength,
            int totalCount)
        {
            applicationDbContext.Setup(x => x.GetArchiveBlogPostCountAsync(
                ItIs.Equal(year),
                ItIs.Equal(month)))
                .ReturnsAsync(totalCount);

            var result = await sut.GetBlogPosts(year, month, pageIndex, pageLength) as OkObjectResult;

            var response = result.Value as PaginatedListResponse<BlogPostDto>;

            response.TotalCount
                .Should()
                .Be(totalCount);
        }
    }
}