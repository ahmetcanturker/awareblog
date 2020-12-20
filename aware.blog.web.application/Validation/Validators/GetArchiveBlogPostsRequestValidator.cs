using Aware.Blog.Contract;

using FluentValidation;

namespace Aware.Blog.Validation.Validators
{
    public class GetArchiveBlogPostsRequestValidator : ListRequestValidator<GetArchiveBlogPostsRequest>
    {
        public GetArchiveBlogPostsRequestValidator()
        {
            RuleFor(x => x.Month)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(12);

            RuleFor(x => x.Year)
                .GreaterThanOrEqualTo(1);
        }
    }
}