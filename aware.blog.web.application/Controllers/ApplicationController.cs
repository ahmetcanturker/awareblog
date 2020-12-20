using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Aware.Blog.Contract;
using Aware.Blog.Domain;
using Aware.Blog.Validation;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aware.Blog.Web.Application.Controllers
{
    [ApiController]
    public abstract class ApplicationController<TController> : ControllerBase
    {
        protected ILogger<TController> Logger { get; }
        protected IApplicationDbContext ApplicationDbContext { get; }
        protected IGenericValidator GenericValidator { get; }
        protected IMapper Mapper { get; }

        public ApplicationController(
            ILogger<TController> logger,
            IApplicationDbContext applicationDbContext,
            IGenericValidator genericValidator,
            IMapper mapper)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ApplicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            GenericValidator = genericValidator ?? throw new ArgumentNullException(nameof(genericValidator));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected virtual IActionResult Error(Error error)
        {
            return StatusCode(error.StatusCode, new Response(new List<Error> { error })
            {
                StatusCode = error.StatusCode
            });
        }

        protected virtual IActionResult Success() => Ok(new Response());
        protected virtual IActionResult Data<T>(T data) => Ok(new DataResponse<T>(data));
        protected virtual IActionResult List<T>(IList<T> data) => Ok(new ListResponse<T>(data));
        protected virtual IActionResult PaginatedList<T>(IList<T> data, int totalCount) => Ok(new PaginatedListResponse<T>(data, totalCount));

        #region IGenericValidator Members
        protected virtual void Validate<T>(T obj) => GenericValidator.Validate(obj);
        protected virtual ValidationResult ValidateResult<T>(T obj) => GenericValidator.ValidateResult(obj);
        protected virtual async Task ValidateAsync<T>(T obj) => await GenericValidator.ValidateAsync(obj);
        protected virtual async Task<ValidationResult> ValidateResultAsync<T>(T obj) => await GenericValidator.ValidateResultAsync(obj);
        #endregion

        #region IMapper Members
        protected virtual TDestination Map<TDestination>(object source) => Mapper.Map<TDestination>(source);
        #endregion
    }
}