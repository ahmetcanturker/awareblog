using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
    public class ValidateResultAsyncTests
    {
        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Throws_If_Service_Is_Null<T>(
            Mock<IServiceProvider> serviceProvider,
            SUT sut,
            Fake.Request request)
        {
            serviceProvider.Setup(x => x.GetService(It.IsAny<Type>()))
                .Returns(null);

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await sut.ValidateResultAsync(request);
            });
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Validates_Request(
            Mock<IValidator<Fake.Request>> requestValidator,
            SUT sut,
            Fake.Request request)
        {
            requestValidator.Setup(x => x.ValidateAsync(ItIs.Equal(request), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            await sut.ValidateResultAsync(request);

            requestValidator.Verify(x => x.ValidateAsync(ItIs.Equal(request), It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Theory]
        [AutoMoqData(typeof(UnitCustomization))]
        public async Task Returns_Invalid_Result_When_Request_Is_Invalid(
            Mock<IValidator<Fake.Request>> requestValidator,
            SUT sut,
            Fake.Request request,
            IList<ValidationFailure> validationFailures)
        {
            requestValidator.Setup(x => x.ValidateAsync(ItIs.Equal(request), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(validationFailures));

            var result = await sut.ValidateResultAsync(request);

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
        public async Task Returns_Valid_Result_When_Request_Is_Valid(
            Mock<IValidator<Fake.Request>> requestValidator,
            SUT sut,
            Fake.Request request)
        {
            requestValidator.Setup(x => x.ValidateAsync(ItIs.Equal(request), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var result = await sut.ValidateResultAsync(request);

            result.IsValid
                .Should()
                .BeTrue();

            result.Errors.Count
                .Should()
                .Be(0);
        }
    }
}