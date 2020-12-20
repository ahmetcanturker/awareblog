using System.Collections.Generic;
using System.Linq;

using AutoFixture;

using Aware.Blog.Contract;
using Aware.Blog.Domain;

using Moq;

namespace Aware.Blog.Web.Application.Tests.ArchiveController
{
    public class GetArchivesCustomization : ArchiveControllerCustomization
    {
        public override void Customize(IFixture fixture)
        {
            base.Customize(fixture);

            var archives = fixture.CreateMany<ArchiveModel>()
                .ToList();

            ApplicationDbContext.Setup(x => x.GetArchivesAsync())
                .ReturnsAsync(archives);

            var archiveDtos = fixture.CreateMany<ArchiveDto>(archives.Count)
                .ToList();

            Mapper.Setup(x => x.Map<IList<ArchiveDto>>(It.IsAny<object>()))
                .Returns(archiveDtos);
        }
    }
}