using AutoMapper;
using PRN231.Application.Services.AuthServices.Dtos;
using PRN231.Domain.Entities;
using PRN231.Domain.Models;

namespace PRN231.Application.Services.AuthServices.MappingProfiles;

public class AuthMappingProfiles : Profile
{
    public AuthMappingProfiles()
    {
        CreateMap<SignUpRequestDto, User>();
        CreateMap<LogInRequestDto, User>();

        CreateMap<User, JwtModel>();

        CreateMap<User, LogInResponseDto>()
            .ForMember(
                dest => dest.UserName,
                opt => opt.MapFrom(src => src.Name)
            );
    }
}
