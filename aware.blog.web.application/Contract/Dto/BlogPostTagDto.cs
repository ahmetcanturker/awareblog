namespace Aware.Blog.Contract
{
    public class BlogPostTagDto : EntityDto<int>
    {
        public int BlogPostId { get; set; }
        public int TagId { get; set; }

        public BlogPostDto BlogPost { get; set; }
        public TagDto Tag { get; set; }
    }
}