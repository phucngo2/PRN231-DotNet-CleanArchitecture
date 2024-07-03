using AutoMapper;
using PRN231.Application.Services.AnimeServices.Dtos;
using PRN231.Domain.Entities;
using PRN231.Domain.Exceptions.Anime;
using PRN231.Domain.Interfaces.UnitOfWork;
using PRN231.Domain.Models;

namespace PRN231.Application.Services.AnimeServices;

public class AnimeService(IMapper mapper, IUnitOfWork unitOfWork) : IAnimeService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddAsync(AnimeUpsertRequestDto request)
    {
        var newAnime = _mapper.Map<Anime>(request);

        var genres = await _unitOfWork.GenreRepository.ListGenresByIds(request.GenreIds);
        newAnime.Genres = genres;

        await _unitOfWork.AnimeRepository.AddAsync(newAnime);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var exisitingAnime = await _unitOfWork.AnimeRepository.GetByIdAsync(id)
            ?? throw new AnimeNotFoundException();

        _unitOfWork.AnimeRepository.Delete(exisitingAnime);
        await _unitOfWork.CommitAsync();
    }

    public async Task<AnimeDetailResponseDto> GetAsync(int id)
    {
        var anime = await _unitOfWork.AnimeRepository.GetAnimeById(id)
            ?? throw new AnimeNotFoundException();
        var response = _mapper.Map<AnimeDetailResponseDto>(anime);
        return response;
    }

    public async Task<List<AnimeResponseDto>> ListAsync()
    {
        var animes = await _unitOfWork.AnimeRepository.ListAsync();
        var response = _mapper.Map<List<AnimeResponseDto>>(animes);
        return response;
    }

    public async Task UpdateAsync(int id, AnimeUpsertRequestDto request)
    {
        var exisitingAnime = await _unitOfWork.AnimeRepository.GetAnimeById(id)
            ?? throw new AnimeNotFoundException();

        var updatedAnime = _mapper.Map(request, exisitingAnime);
        var genres = await _unitOfWork.GenreRepository.ListGenresByIds(request.GenreIds);
        updatedAnime.Genres = genres;

        _unitOfWork.AnimeRepository.Update(updatedAnime);
        await _unitOfWork.CommitAsync();
    }

    public async Task<PaginationResponse<AnimeResponseDto>> PaginateAsync(PaginationRequest request)
    {
        var paginatedAnimes = await _unitOfWork.AnimeRepository.PaginateAsync(request);
        var response = _mapper.Map<PaginationResponse<AnimeResponseDto>>(paginatedAnimes);
        return response;
    }
}
