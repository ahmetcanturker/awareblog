using AutoFixture;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Aware.Blog.Web.Application.Tests
{
    public class ControllerCustomization : ICustomization
    {
        public virtual void Customize(IFixture fixture)
        {
            fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());
        }
    }
}