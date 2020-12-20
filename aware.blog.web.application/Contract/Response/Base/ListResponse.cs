using System.Collections.Generic;

namespace Aware.Blog.Contract
{
    public class ListResponse<T> : DataResponse<IList<T>>
    {
        public ListResponse(IList<T> data) : base(data)
        { }

        public ListResponse(IList<Error> errors) : base(errors)
        { }
    }
}