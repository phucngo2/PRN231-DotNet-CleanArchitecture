using AutoMapper;
using PRN231.Application.Services.AnimeServices.Dtos;
using PRN231.Domain.Entities;
using PRN231.Domain.Models;

namespace PRN231.Application.Services.AnimeServices.MappingProfiles;

public class AnimeMappingProfiles : Profile
{
    public AnimeMappingProfiles()
    {
        CreateMap<AnimeUpsertRequestDto, Anime>();
        CreateMap<Anime, AnimeResponseDto>();
        CreateMap<Anime, AnimeDetailResponseDto>();
        CreateMap<PaginationResponse<Anime>, PaginationResponse<AnimeResponseDto>>();
    }
}
