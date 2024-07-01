using AutoMapper;
using PRN231.Application.Services.GenreServices.Dtos;
using PRN231.Domain.Entities;

namespace PRN231.Application.Services.GenreServices.MappingProfiles;

public class GenreMappingProfiles : Profile
{
    public GenreMappingProfiles()
    {
        CreateMap<GenreUpsertRequestDto, Genre>();
        CreateMap<Genre, GenreResponseDto>();
        CreateMap<Genre, GenreDetailResponseDto>();
    }
}
