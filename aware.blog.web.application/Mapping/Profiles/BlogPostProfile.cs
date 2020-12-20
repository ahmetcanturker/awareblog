using AutoMapper;

using Aware.Blog.Contract;
using Aware.Blog.Domain;

namespace Aware.Blog.Mapping.Profiles
{
    class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogPostDto>();
        }
    }
}