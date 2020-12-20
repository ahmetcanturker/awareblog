using System;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.Extensions.DependencyInjection;

namespace Aware.Blog.Validation
{
    public class GenericValidator : IGenericValidator
    {
        protected virtual IServiceProvider ServiceProvider { get; }

        public GenericValidator(
            IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected virtual T GetService<T>()
            where T : class
        {
            return ServiceProvider.GetService(typeof(T)) as T;
        }

        protected virtual T GetScopedService<T>()
            where T : class
        {
            return ServiceProvider.CreateScope().ServiceProvider.GetService(typeof(T)) as T;
        }

        protected virtual IValidator<T> GetValidator<T>() => GetService<IValidator<T>>();

        public async Task ValidateAsync<T>(T obj)
        {
            var validator = GetValidator<T>();

            if (validator == null)
                throw new Exception("Validator not found for type: " + typeof(T).Name);

            var validationResult = await validator.ValidateAsync(obj);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }

        public async Task<ValidationResult> ValidateResultAsync<T>(T obj)
        {
            var validator = GetValidator<T>();

            if (validator == null)
                throw new Exception("Validator not found for type: " + typeof(T).Name);

            var validationResult = await validator.ValidateAsync(obj);

            return validationResult;
        }

        public virtual void Validate<T>(T obj)
        {
            var validator = GetValidator<T>();

            if (validator == null)
                throw new Exception("Validator not found for type: " + typeof(T).Name);

            var validationResult = validator.Validate(obj);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }

        public virtual ValidationResult ValidateResult<T>(T obj)
        {
            var validator = GetValidator<T>();

            if (validator == null)
                throw new Exception("Validator not found for type: " + typeof(T).Name);

            var validationResult = validator.Validate(obj);

            return validationResult;
        }
    }
}