using AutoMapper;
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

    public async Task AddAsync(GenreUpsertRequestDto request)
    {
        var newGenre = _mapper.Map<Genre>(request);
        await _unitOfWork.GenreRepository.AddAsync(newGenre);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var exisitingGenre = await _unitOfWork.GenreRepository.GetByIdAsync(id) 
            ?? throw new GenreNotFoundException();

        _unitOfWork.GenreRepository.Delete(exisitingGenre);
        await _unitOfWork.CommitAsync();
    }

    public async Task<GenreDetailResponseDto> GetAsync(int id)
    {
        var anime = await _unitOfWork.GenreRepository.GetGenreWithAnimesAsync(id)
            ?? throw new GenreNotFoundException();
        var response = _mapper.Map<GenreDetailResponseDto>(anime);
        return response;
    }

    public async Task<List<GenreResponseDto>> ListAsync()
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

    public async Task<List<GenreResponseDto>> ListSoftDeletedAsync()
    {
        var genres = await _unitOfWork.GenreRepository.ListSoftDeletedAsync();
        var response = _mapper.Map<List<GenreResponseDto>>(genres);
        return response;
    }

    public async Task UpdateAsync(int id, GenreUpsertRequestDto request)
    {
        var exisitingGenre = await _unitOfWork.GenreRepository.GetByIdAsync(id)
            ?? throw new GenreNotFoundException();

        var updatedGenre = _mapper.Map(request, exisitingGenre);

        _unitOfWork.GenreRepository.Update(updatedGenre);
        await _unitOfWork.CommitAsync();
    }
}
