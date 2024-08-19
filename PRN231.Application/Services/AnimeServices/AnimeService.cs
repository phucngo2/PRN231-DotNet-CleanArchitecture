using AutoMapper;
using LanguageExt.Common;
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

    public async Task<Result<bool>> AddAsync(AnimeUpsertRequestDto request)
    {
        var newAnime = _mapper.Map<Anime>(request);

        var genres = await _unitOfWork.GenreRepository.ListGenresByIds(request.GenreIds);
        newAnime.Genres = genres;

        await _unitOfWork.AnimeRepository.AddAsync(newAnime);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var existingAnime = await _unitOfWork.AnimeRepository.GetByIdAsync(id);
        if (existingAnime == null)
        {
            return new Result<bool>(new AnimeNotFoundException());
        }

        _unitOfWork.AnimeRepository.Delete(existingAnime);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<Result<AnimeDetailResponseDto>> GetAsync(int id)
    {
        var anime = await _unitOfWork.AnimeRepository.GetAnimeById(id);
        if (anime == null)
        {
            return new Result<AnimeDetailResponseDto>(new AnimeNotFoundException());
        }

        var response = _mapper.Map<AnimeDetailResponseDto>(anime);
        return response;
    }

    public async Task<Result<List<AnimeResponseDto>>> ListAsync()
    {
        var animes = await _unitOfWork.AnimeRepository.ListAsync();
        var response = _mapper.Map<List<AnimeResponseDto>>(animes);
        return response;
    }

    public async Task<Result<bool>> UpdateAsync(int id, AnimeUpsertRequestDto request)
    {
        var existingAnime = await _unitOfWork.AnimeRepository.GetAnimeById(id);
        if (existingAnime == null)
        {
            return new Result<bool>(new AnimeNotFoundException());
        }

        var updatedAnime = _mapper.Map(request, existingAnime);
        var genres = await _unitOfWork.GenreRepository.ListGenresByIds(request.GenreIds);
        updatedAnime.Genres = genres;

        _unitOfWork.AnimeRepository.Update(updatedAnime);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<Result<PaginationResponse<AnimeResponseDto>>> PaginateAsync(PaginationRequest request)
    {
        var paginatedAnimes = await _unitOfWork.AnimeRepository.PaginateAsync(request);
        var response = _mapper.Map<PaginationResponse<AnimeResponseDto>>(paginatedAnimes);
        return response;
    }

    public async Task<Result<PaginationResponse<AnimeResponseDto>>> PaginateSoftDeletedAsync(PaginationRequest request)
    {
        var paginatedAnimes = await _unitOfWork.AnimeRepository.PaginateSoftDeletedAsync(request);
        var response = _mapper.Map<PaginationResponse<AnimeResponseDto>>(paginatedAnimes);
        return response;
    }
}
