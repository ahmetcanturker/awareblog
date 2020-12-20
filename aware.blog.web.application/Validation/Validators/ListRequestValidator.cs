using Aware.Blog.Contract;

using FluentValidation;

namespace Aware.Blog.Validation.Validators
{
    public class ListRequestValidator : ListRequestValidator<ListRequest>
    {
    }

    public abstract class ListRequestValidator<TRequest> : AbstractValidator<TRequest>
        where TRequest : ListRequest
    {
        public ListRequestValidator()
        {
            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.PageLength)
                .GreaterThan(0)
                .LessThan(100);
        }
    }
}