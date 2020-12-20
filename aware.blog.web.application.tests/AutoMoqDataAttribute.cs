using System;

using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Aware.Blog.Web.Application.Tests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Func<IFixture>(() => new Fixture()))
        { }

        public AutoMoqDataAttribute(Type customizationType)
            : base(new Func<IFixture>(() => new Fixture()
                .Customize(new AutoMoqCustomization())
                .Customize(Activator.CreateInstance(customizationType) as ICustomization)))
        { }
    }
}
