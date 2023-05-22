using AutoMapper;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Persistence.AutoMapperProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Permissions, PermissionsDto>()
                .ForMember(dest => dest.PermissionTypes, opt => opt.MapFrom(src => src.PermissionTypes))
                .ReverseMap();

            CreateMap<PermissionTypes, PermissionTypesDto>().ReverseMap(); 
        }
    }
}
