namespace Aware.Blog.Domain
{
    public class Image : Entity<int>
    {
        public string Url { get; set; }
        public string Description { get; set; }
    }
}