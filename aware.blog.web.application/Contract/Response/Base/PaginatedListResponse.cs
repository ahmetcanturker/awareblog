using System.Collections.Generic;

namespace Aware.Blog.Contract
{
    public class PaginatedListResponse<T> : ListResponse<T>
    {
        public int TotalCount { get; set; }

        public PaginatedListResponse(IList<T> data) : base(data)
        { }

        public PaginatedListResponse(IList<T> data, int totalCount) : base(data)
        {
            TotalCount = totalCount;
        }

        public PaginatedListResponse(IList<Error> errors) : base(errors)
        { }
    }
}