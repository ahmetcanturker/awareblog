using System.Collections.Generic;
using System.Net;

namespace Aware.Blog.Contract
{
    public class Response
    {
        public IList<Error> Errors { get; set; }
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;
        public bool Success => Errors == null || Errors.Count <= 0;

        public Response()
        {
            Errors = new List<Error>();
        }

        public Response(
            IList<Error> errors)
        {
            Errors = new List<Error>(errors);
        }
    }
}