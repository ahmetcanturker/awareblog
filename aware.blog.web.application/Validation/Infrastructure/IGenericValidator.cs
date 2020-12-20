using System.Threading.Tasks;

using FluentValidation.Results;

namespace Aware.Blog.Validation
{
    public interface IGenericValidator
    {
        void Validate<T>(T obj);
        ValidationResult ValidateResult<T>(T obj);

        Task ValidateAsync<T>(T obj);
        Task<ValidationResult> ValidateResultAsync<T>(T obj);
    }
}