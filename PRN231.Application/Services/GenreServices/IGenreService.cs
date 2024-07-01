using PRN231.Application.Services.GenreServices.Dtos;

namespace PRN231.Application.Services.GenreServices;

public interface IGenreService
{
    public Task AddAsync(GenreUpsertRequestDto request);
    public Task DeleteAsync(int id);
    public Task<GenreDetailResponseDto> GetAsync(int id);
    public Task UpdateAsync(int id, GenreUpsertRequestDto request);
    public Task<List<GenreResponseDto>> ListAsync();
}
