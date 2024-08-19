using LanguageExt.Common;
using PRN231.Application.Services.AnimeServices.Dtos;
using PRN231.Domain.Models;

namespace PRN231.Application.Services.AnimeServices;

public interface IAnimeService
{
    public Task AddAsync(AnimeUpsertRequestDto request);
    public Task<Result<bool>> DeleteAsync(int id);
    public Task UpdateAsync(int id, AnimeUpsertRequestDto request);
    public Task<List<AnimeResponseDto>> ListAsync();
    public Task<Result<AnimeDetailResponseDto>> GetAsync(int id);
    Task<PaginationResponse<AnimeResponseDto>> PaginateAsync(PaginationRequest request);
    public Task<PaginationResponse<AnimeResponseDto>> PaginateSoftDeletedAsync(PaginationRequest request);
}
