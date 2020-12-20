using System.Collections.Generic;

namespace Aware.Blog.Contract
{
    public class BlogPostDto : EntityDto<int>
    {
        public int ImageId { get; set; }
        public int AuthorId { get; set; }
        public string Uri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string ContentMarkdown { get; set; }

        public ImageDto Image { get; set; }
        public UserDto Author { get; set; }
        public IList<BlogPostTagDto> Tags { get; set; }
    }
}