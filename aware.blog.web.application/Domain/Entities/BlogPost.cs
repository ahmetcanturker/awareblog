using System.Collections.Generic;

namespace Aware.Blog.Domain
{
    public class BlogPost : Entity<int>
    {
        public int ImageId { get; set; }
        public int AuthorId { get; set; }
        public string Uri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string ContentMarkdown { get; set; }

        public Image Image { get; set; }
        public User Author { get; set; }
        public IList<BlogPostTag> Tags { get; set; }

        public BlogPost()
        {
            Tags = new List<BlogPostTag>();
        }
    }
}