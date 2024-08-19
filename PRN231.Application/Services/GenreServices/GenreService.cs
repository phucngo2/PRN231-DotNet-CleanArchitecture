using AutoMapper;
using LanguageExt.Common;
using PRN231.Application.Services.GenreServices.Dtos;
using PRN231.Domain.Constants;
using PRN231.Domain.Entities;
using PRN231.Domain.Exceptions.Genre;
using PRN231.Domain.Interfaces.Cache;
using PRN231.Domain.Interfaces.UnitOfWork;

namespace PRN231.Application.Services.GenreServices;

public class GenreService(IMapper mapper, IUnitOfWork unitOfWork, IRedisService redisService) : IGenreService
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRedisService _redisService = redisService;

    public async Task<Result<bool>> AddAsync(GenreUpsertRequestDto request)
    {
        var newGenre = _mapper.Map<Genre>(request);
        await _unitOfWork.GenreRepository.AddAsync(newGenre);
        await _unitOfWork.CommitAsync();
        await _redisService.RemoveAsync(CacheConstants.Genres.KEY);
        return true;
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var existingGenre = await _unitOfWork.GenreRepository.GetByIdAsync(id);
        if (existingGenre == null)
        {
            return new Result<bool>(new GenreNotFoundException());
        }

        _unitOfWork.GenreRepository.Delete(existingGenre);
        await _unitOfWork.CommitAsync();
        await _redisService.RemoveAsync(CacheConstants.Genres.KEY);
        return true;
    }

    public async Task<Result<GenreDetailResponseDto>> GetAsync(int id)
    {
        var anime = await _unitOfWork.GenreRepository.GetGenreWithAnimesAsync(id);
        if (anime == null)
        {
            return new Result<GenreDetailResponseDto>(new GenreNotFoundException());
        }

        var response = _mapper.Map<GenreDetailResponseDto>(anime);
        return response;
    }

    public async Task<Result<List<GenreResponseDto>>> ListAsync()
    {
        var cachedGenres = await _redisService.GetAsync<List<GenreResponseDto>>(CacheConstants.Genres.KEY);
        if (cachedGenres is not default(List<GenreResponseDto>))
        {
            return cachedGenres;
        }
        var genres = await _unitOfWork.GenreRepository.ListAsync();
        var response = _mapper.Map<List<GenreResponseDto>>(genres);
        await _redisService.SetAsync(CacheConstants.Genres.KEY, response, CacheConstants.Genres.EXPIRY);
        return response;
    }

    public async Task<Result<List<GenreResponseDto>>> ListSoftDeletedAsync()
    {
        var genres = await _unitOfWork.GenreRepository.ListSoftDeletedAsync();
        var response = _mapper.Map<List<GenreResponseDto>>(genres);
        return response;
    }

    public async Task<Result<bool>> UpdateAsync(int id, GenreUpsertRequestDto request)
    {
        var existingGenre = await _unitOfWork.GenreRepository.GetByIdAsync(id);
        if (existingGenre == null)
        {
            return new Result<bool>(new GenreNotFoundException());
        }

        var updatedGenre = _mapper.Map(request, existingGenre);

        _unitOfWork.GenreRepository.Update(updatedGenre);
        await _unitOfWork.CommitAsync();
        await _redisService.RemoveAsync(CacheConstants.Genres.KEY);
        return true;
    }
}
