namespace Aware.Blog.Contract
{
    public class GetArchiveBlogPostsRequest : ListRequest
    {
        public int Year { get; set; }
        public int Month { get; set; }
    }
}