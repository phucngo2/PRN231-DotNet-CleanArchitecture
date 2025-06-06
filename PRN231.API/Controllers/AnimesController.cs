﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231.Application.Services.AnimeServices;
using PRN231.Application.Services.AnimeServices.Dtos;
using PRN231.Domain.Enums;
using PRN231.Domain.Extensions;
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
        return res.ToResult();
    }

    [HttpGet("deleted")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> ListDeleted([FromQuery] PaginationRequest request)
    {
        var res = await _animeService.PaginateSoftDeletedAsync(request);
        return res.ToResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var res = await _animeService.GetAsync(id);
        return res.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Create([FromBody] AnimeUpsertRequestDto request)
    {
        var res = await _animeService.AddAsync(request);
        return res.ToResult();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AnimeUpsertRequestDto request)
    {
        var res = await _animeService.UpdateAsync(id, request);
        return res.ToResult();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var res = await _animeService.DeleteAsync(id);
        return res.ToResult();
    }
}
