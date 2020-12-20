using System;
using System.Collections.Generic;

using FluentValidation.Results;

namespace Aware.Blog.Validation
{
    public class ValidationException : Exception
    {
        public IList<ValidationFailure> Errors { get; }

        public ValidationException(
            IList<ValidationFailure> errors)
        {
            Errors = errors;
        }
    }
}