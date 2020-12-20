using AutoFixture;

using Aware.Blog.Web.Application.Tests;

using FluentValidation;

using Moq;

namespace Aware.Blog.Tests.GenericValidator
{
    public class ValidateCustomization : GenericValidatorCustomization
    {
        public override void Customize(IFixture fixture)
        {
            base.Customize(fixture);

            var requestValidator = fixture.Freeze<Mock<IValidator<Fake.Request>>>();

            ServiceProvider.Setup(x => x.GetService(ItIs.Equal(typeof(IValidator<Fake.Request>))))
                .Returns(requestValidator.Object);
        }
    }
}