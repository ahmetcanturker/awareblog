using AutoMapper;

using Aware.Blog.Contract;
using Aware.Blog.Domain;

namespace Aware.Blog.Mapping.Profiles
{
    class BlogPostTagProfile : Profile
    {
        public BlogPostTagProfile()
        {
            CreateMap<BlogPostTag, BlogPostTagDto>();
        }
    }
}