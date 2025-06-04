using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231.Application.Services.GenreServices;
using PRN231.Application.Services.GenreServices.Dtos;
using PRN231.Domain.Enums;
using PRN231.Domain.Extensions;

namespace PRN231.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController(IGenreService genreService) : ControllerBase
{
    private readonly IGenreService _genreService = genreService;

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var res = await _genreService.ListAsync();
        return res.ToResult();
    }

    [HttpGet("deleted")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> ListDeleted()
    {
        var res = await _genreService.ListSoftDeletedAsync();
        return res.ToResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var res = await _genreService.GetAsync(id);
        return res.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Create([FromBody] GenreUpsertRequestDto request)
    {
        var res = await _genreService.AddAsync(request);
        return res.ToResult();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GenreUpsertRequestDto request)
    {
        var res = await _genreService.UpdateAsync(id, request);
        return res.ToResult();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var res = await _genreService.DeleteAsync(id);
        return res.ToResult();
    }
}
