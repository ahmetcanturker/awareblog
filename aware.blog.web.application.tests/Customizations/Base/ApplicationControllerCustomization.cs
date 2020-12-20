using System;
using System.Collections.Generic;

using AutoFixture;

using AutoMapper;

using Aware.Blog.Domain;
using Aware.Blog.Validation;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Logging;

using Moq;

namespace Aware.Blog.Web.Application.Tests
{
    public class ApplicationControllerCustomization<TController> : ControllerCustomization
    {
        public Mock<ILogger<TController>> Logger { get; private set; }
        public Mock<IApplicationDbContext> ApplicationDbContext { get; private set; }
        public Mock<IGenericValidator> GenericValidator { get; private set; }
        public Mock<IMapper> Mapper { get; private set; }

        public override void Customize(IFixture fixture)
        {
            base.Customize(fixture);

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            #region ILogger
            Logger = fixture.Freeze<Mock<ILogger<TController>>>();
            fixture.Register<ILogger<TController>>(() => Logger.Object);
            #endregion

            #region IApplicationDbContext
            ApplicationDbContext = fixture.Freeze<Mock<IApplicationDbContext>>();
            fixture.Register<IApplicationDbContext>(() => ApplicationDbContext.Object);
            #endregion

            #region IGenericValidator
            GenericValidator = fixture.Freeze<Mock<IGenericValidator>>();

            GenericValidator.Setup(x => x.Validate(It.IsAny<object>()));

            GenericValidator.Setup(x => x.ValidateAsync(It.IsAny<object>()));

            GenericValidator.Setup(x => x.ValidateResult(It.IsAny<object>()))
                .Returns(new ValidationResult(new List<ValidationFailure>()));

            GenericValidator.Setup(x => x.ValidateResultAsync(It.IsAny<object>()))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>()));

            fixture.Register<IGenericValidator>(() => GenericValidator.Object);
            #endregion

            #region IMapper
            Mapper = fixture.Freeze<Mock<IMapper>>();

            Mapper.Setup(x => x.Map<It.IsAnyType>(It.IsAny<object>()))
                .Returns(new InvocationFunc((invocation) => Activator.CreateInstance(invocation.Method.GetGenericArguments()[0])));
            #endregion
        }

        private class CustomCompositeMetadataDetailsProvider : ICompositeMetadataDetailsProvider
        {
            public void CreateBindingMetadata(BindingMetadataProviderContext context)
            {
                throw new System.NotImplementedException();
            }

            public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
            {
                throw new System.NotImplementedException();
            }

            public void CreateValidationMetadata(ValidationMetadataProviderContext context)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}