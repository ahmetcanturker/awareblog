using AutoFixture;

using SUT = Aware.Blog.Web.Application.Controllers.ArchiveController;

namespace Aware.Blog.Web.Application.Tests.ArchiveController
{
    public class ArchiveControllerCustomization : ApplicationControllerCustomization<SUT>
    {
        public override void Customize(IFixture fixture)
        {
            base.Customize(fixture);
        }
    }
}