namespace Aware.Blog.Domain
{
    public class BlogPostTag : Entity<int>
    {
        public int BlogPostId { get; set; }
        public int TagId { get; set; }

        public BlogPost BlogPost { get; set; }
        public Tag Tag { get; set; }
    }
}