using System;
using System.Collections.Generic;

using Aware.Blog.Web.Application.Tests;

using FluentValidation;
using FluentValidation.Results;

using Moq;

using Xunit;

using SUT = Aware.Blog.Validation.GenericValidator;
using UnitCustomization = Aware.Blog.Tests.GenericValidator.ValidateCustomization;

namespace Aware.Blog.Tests.BlogPostController
{
    [Trait("Tests", "GenericValidator Unit Tests")]
    public class ValidateTests
    {
        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public void Throws_If_Service_Is_Null<T>(
            Mock<IServiceProvider> serviceProvider,
            SUT sut,
            Fake.Request request)
        {
            serviceProvider.Setup(x => x.GetService(It.IsAny<Type>()))
                .Returns(null);

            Assert.Throws<Exception>(() =>
            {
                sut.Validate(request);
            });
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public void Validates_Request(
            Mock<IValidator<Fake.Request>> requestValidator,
            SUT sut,
            Fake.Request request)
        {
            requestValidator.Setup(x => x.Validate(ItIs.Equal(request)))
                .Returns(new ValidationResult());

            sut.Validate(request);

            requestValidator.Verify(x => x.Validate(ItIs.Equal(request)),
                Times.Once());
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public void Throws_When_Request_Is_Invalid(
            Mock<IValidator<Fake.Request>> requestValidator,
            SUT sut,
            Fake.Request request,
            IList<ValidationFailure> validationFailures)
        {
            requestValidator.Setup(x => x.Validate(ItIs.Equal(request)))
                .Returns(new ValidationResult(validationFailures));

            Assert.Throws<Validation.ValidationException>(() =>
            {
                sut.Validate(request);
            });
        }
    }
}