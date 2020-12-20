using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

namespace Aware.Blog.Validation
{
    public class FluentValidationInterceptor : IValidatorInterceptor
    {
        public ValidationResult AfterMvcValidation(
            ControllerContext controllerContext,
            IValidationContext commonContext,
            ValidationResult result)
        {
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            return result;
        }

        public IValidationContext BeforeMvcValidation(
            ControllerContext controllerContext,
            IValidationContext commonContext)
        {
            return commonContext;
        }
    }
}