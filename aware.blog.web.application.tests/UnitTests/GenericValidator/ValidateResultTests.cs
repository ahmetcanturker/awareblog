using System;
using System.Collections.Generic;

using Aware.Blog.Web.Application.Tests;

using FluentAssertions;

using FluentValidation;
using FluentValidation.Results;

using Moq;

using Xunit;

using SUT = Aware.Blog.Validation.GenericValidator;
using UnitCustomization = Aware.Blog.Tests.GenericValidator.ValidateCustomization;

namespace Aware.Blog.Tests.BlogPostController
{
    [Trait("Tests", "GenericValidator Unit Tests")]
    public class ValidateResultTests
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
                sut.ValidateResult(request);
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

            sut.ValidateResult(request);

            requestValidator.Verify(x => x.Validate(ItIs.Equal(request)),
                Times.Once());
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public void Returns_Invalid_Result_When_Request_Is_Invalid(
            Mock<IValidator<Fake.Request>> requestValidator,
            SUT sut,
            Fake.Request request,
            IList<ValidationFailure> validationFailures)
        {
            requestValidator.Setup(x => x.Validate(ItIs.Equal(request)))
                .Returns(new ValidationResult(validationFailures));

            var result = sut.ValidateResult(request);

            result.IsValid
                .Should()
                .BeFalse();

            result.Errors.Count
                .Should()
                .Be(validationFailures.Count);

            result.Errors
                .Should()
                .BeEquivalentTo(validationFailures);
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public void Returns_Valid_Result_When_Request_Is_Valid(
            Mock<IValidator<Fake.Request>> requestValidator,
            SUT sut,
            Fake.Request request)
        {
            requestValidator.Setup(x => x.Validate(ItIs.Equal(request)))
                .Returns(new ValidationResult());

            var result = sut.ValidateResult(request);

            result.IsValid
                .Should()
                .BeTrue();

            result.Errors.Count
                .Should()
                .Be(0);
        }
    }
}