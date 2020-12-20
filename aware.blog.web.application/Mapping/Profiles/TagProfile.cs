using AutoMapper;

using Aware.Blog.Contract;
using Aware.Blog.Domain;

namespace Aware.Blog.Mapping.Profiles
{
    class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDto>();
        }
    }
}