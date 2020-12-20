namespace Aware.Blog.Domain
{
    public class Tag : Entity<int>
    {
        public string Uri { get; set; }
        public string Name { get; set; }
    }
}