namespace Aware.Blog.Contract
{
    public class TagDto : EntityDto<int>
    {
        public string Uri { get; set; }
        public string Name { get; set; }
    }
}