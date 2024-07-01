using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231.Application.Services.GenreServices;
using PRN231.Application.Services.GenreServices.Dtos;
using PRN231.Domain.Enums;
using PRN231.Domain.Exceptions.Common;

namespace PRN231.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController(IGenreService genreService) : ControllerBase
{
    private readonly IGenreService _genreService = genreService;

    [HttpGet]
    public async Task<IActionResult> List()
    {
        try
        {
            var res = await _genreService.ListAsync();
            return Ok(res);
        } catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var res = await _genreService.GetAsync(id);
            return Ok(res);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Create([FromBody] GenreUpsertRequestDto request)
    {
        try
        {
            await _genreService.AddAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GenreUpsertRequestDto request)
    {
        try
        {
            await _genreService.UpdateAsync(id, request);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _genreService.DeleteAsync(id);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
