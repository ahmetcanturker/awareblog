using System.Collections.Generic;
using System.Threading.Tasks;

using Aware.Blog.Contract;
using Aware.Blog.Domain;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

using SUT = Aware.Blog.Web.Application.Controllers.ArchiveController;
using UnitCustomization = Aware.Blog.Web.Application.Tests.ArchiveController.GetArchivesCustomization;

namespace Aware.Blog.Web.Application.Tests.ArchiveController
{
    [Trait("Tests", "ArchiveController Unit Tests")]
    public class GetArchivesTests
    {
        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Ok_Result(
            SUT sut)
        {
            var result = await sut.GetArchives();

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
            var result = await sut.GetArchives();

            (result as OkObjectResult).Value.Should()
                .NotBeNull();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Successful(
            SUT sut)
        {
            var result = await sut.GetArchives() as OkObjectResult;

            var response = result.Value as Response;

            response.Success
                .Should()
                .BeTrue();
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Gets_Archives(
            Mock<IApplicationDbContext> applicationDbContext,
            SUT sut,
            IList<ArchiveModel> archives)
        {
            applicationDbContext.Setup(x => x.GetArchivesAsync())
                .ReturnsAsync(archives);

            var result = await sut.GetArchives() as OkObjectResult;

            var response = result.Value as ListResponse<ArchiveDto>;

            response.Data.Count
                .Should()
                .Be(archives.Count);
        }
    }
}