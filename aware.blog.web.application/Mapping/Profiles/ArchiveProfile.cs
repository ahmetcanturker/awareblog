using AutoMapper;

using Aware.Blog.Contract;
using Aware.Blog.Domain;

namespace Aware.Blog.Mapping.Profiles
{
    class ArchiveProfile : Profile
    {
        public ArchiveProfile()
        {
            CreateMap<ArchiveModel, ArchiveDto>();
        }
    }
}