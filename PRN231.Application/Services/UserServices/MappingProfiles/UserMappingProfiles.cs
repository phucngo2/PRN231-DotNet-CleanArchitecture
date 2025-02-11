using AutoMapper;
using PRN231.Application.Services.UserServices.Dtos;
using PRN231.Domain.Entities;

namespace PRN231.Application.Services.UserServices.MappingProfiles;

public class UserMappingProfiles : Profile
{
    public UserMappingProfiles()
    {
        CreateMap<User, UserResponseDto>();
    }
}
