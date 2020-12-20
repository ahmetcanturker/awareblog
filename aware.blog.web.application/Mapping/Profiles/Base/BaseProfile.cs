using AutoMapper;

using Aware.Blog.Contract;
using Aware.Blog.Domain;

namespace Aware.Blog.Mapping.Profiles
{
    class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap(typeof(Entity<>), typeof(EntityDto<>))
                .IncludeAllDerived();
        }
    }
}