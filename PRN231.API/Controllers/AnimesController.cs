using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231.Application.Services.AnimeServices;
using PRN231.Application.Services.AnimeServices.Dtos;
using PRN231.Domain.Enums;
using PRN231.Domain.Models;

namespace PRN231.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimesController(IAnimeService animeService) : ControllerBase
{
    private readonly IAnimeService _animeService = animeService;

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PaginationRequest request)
    {
        var res = await _animeService.PaginateAsync(request);
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var res = await _animeService.GetAsync(id);
        return Ok(res);
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Create([FromBody] AnimeUpsertRequestDto request)
    {
        await _animeService.AddAsync(request);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AnimeUpsertRequestDto request)
    {
        await _animeService.UpdateAsync(id, request);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _animeService.DeleteAsync(id);
        return Ok();
    }
}
