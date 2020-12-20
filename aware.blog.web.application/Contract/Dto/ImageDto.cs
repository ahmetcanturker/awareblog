namespace Aware.Blog.Contract
{
    public class ImageDto : EntityDto<int>
    {
        public string Url { get; set; }
        public string Description { get; set; }
    }
}