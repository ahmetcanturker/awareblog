using AutoFixture;

using SUT = Aware.Blog.Web.Application.Controllers.BlogPostController;

namespace Aware.Blog.Web.Application.Tests.BlogPostController
{
    public class BlogPostControllerCustomization : ApplicationControllerCustomization<SUT>
    {
        public override void Customize(IFixture fixture)
        {
            base.Customize(fixture);
        }
    }
}