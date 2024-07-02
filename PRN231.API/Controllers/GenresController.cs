using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231.Application.Services.GenreServices;
using PRN231.Application.Services.GenreServices.Dtos;
using PRN231.Domain.Enums;

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
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var res = await _genreService.GetAsync(id);
        return Ok(res);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Create([FromBody] GenreUpsertRequestDto request)
    {
        await _genreService.AddAsync(request);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GenreUpsertRequestDto request)
    {
        await _genreService.UpdateAsync(id, request);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _genreService.DeleteAsync(id);
        return Ok();
    }
}
