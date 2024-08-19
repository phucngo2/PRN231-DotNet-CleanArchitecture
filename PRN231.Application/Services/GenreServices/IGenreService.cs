using LanguageExt.Common;
using PRN231.Application.Services.GenreServices.Dtos;

namespace PRN231.Application.Services.GenreServices;

public interface IGenreService
{
    public Task<Result<bool>> AddAsync(GenreUpsertRequestDto request);
    public Task<Result<bool>> DeleteAsync(int id);
    public Task<Result<GenreDetailResponseDto>> GetAsync(int id);
    public Task<Result<bool>> UpdateAsync(int id, GenreUpsertRequestDto request);
    public Task<Result<List<GenreResponseDto>>> ListAsync();
    public Task<Result<List<GenreResponseDto>>> ListSoftDeletedAsync();
}
