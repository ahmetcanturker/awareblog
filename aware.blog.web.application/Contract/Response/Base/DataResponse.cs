using System.Collections.Generic;

namespace Aware.Blog.Contract
{
    public class DataResponse<T> : Response
    {
        public T Data { get; set; }

        public DataResponse(
            T data)
        {
            Data = data;
        }

        public DataResponse(IList<Error> errors) : base(errors)
        { }
    }
}