using LanguageExt.Common;
using PRN231.Application.Services.AnimeServices.Dtos;
using PRN231.Domain.Models;

namespace PRN231.Application.Services.AnimeServices;

public interface IAnimeService
{
    public Task<Result<bool>> AddAsync(AnimeUpsertRequestDto request);
    public Task<Result<bool>> DeleteAsync(int id);
    public Task<Result<bool>> UpdateAsync(int id, AnimeUpsertRequestDto request);
    public Task<Result<List<AnimeResponseDto>>> ListAsync();
    public Task<Result<AnimeDetailResponseDto>> GetAsync(int id);
    Task<Result<PaginationResponse<AnimeResponseDto>>> PaginateAsync(PaginationRequest request);
    public Task<Result<PaginationResponse<AnimeResponseDto>>> PaginateSoftDeletedAsync(PaginationRequest request);
}
