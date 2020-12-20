using Aware.Blog.Contract;

using FluentValidation;

namespace Aware.Blog.Validation.Validators
{
    public class GetBlogPostByUriRequestValidator : AbstractValidator<GetBlogPostByUriRequest>
    {
        public GetBlogPostByUriRequestValidator()
        {
            RuleFor(x => x)
                .NotNull();

            RuleFor(x => x.Uri)
                .NotEmpty();
        }
    }
}