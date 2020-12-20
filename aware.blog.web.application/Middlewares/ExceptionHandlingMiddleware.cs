using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Aware.Blog.Contract;
using Aware.Blog.Core;
using Aware.Blog.Validation;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Aware.Blog.Web.Application.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(
            HttpContext context,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            Exception exception = null;

            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (!context.Response.HasStarted)
            {
                if (exception != null)
                {
                    if (exception is ErrorException errorException)
                    {
                        context.Response.StatusCode = errorException.StatusCode;
                        context.Response.ContentType = "application/json";

                        var response = new Response(new List<Error>
                        {
                            new Error
                            {
                                StatusCode = errorException.StatusCode,
                                ErrorCode = errorException.ErrorCode,
                                ErrorMessage = errorException.Message
                            }
                        });

                        var json = JsonConvert.SerializeObject(response);
                        await context.Response.WriteAsync(json);
                    }
                    else if (exception is ValidationException validationException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        context.Response.ContentType = "application/json";

                        var response = new Response(validationException.Errors.Select(x => new ValidationError
                        {
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            ErrorCode = x.ErrorCode,
                            ErrorMessage = x.ErrorMessage,
                            PropertyName = x.PropertyName
                        })
                            .Cast<Error>()
                            .ToList());

                        var json = JsonConvert.SerializeObject(response);
                        await context.Response.WriteAsync(json);
                    }
                    else
                    {
                        logger.LogError(exception, exception.Message);

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var response = new Response(new List<Error>
                        {
                            new Error
                            {
                                ErrorCode = "InternalServerError",
                                ErrorMessage = "Internal Server Error",
                                StatusCode = (int)HttpStatusCode.InternalServerError
                            }
                        });

                        var json = JsonConvert.SerializeObject(response);
                        await context.Response.WriteAsync(json);
                    }
                }
            }
        }
    }
}