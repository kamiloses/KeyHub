using AutoMapper;
using KeyHub.Market.data;
using KeyHub.Market.Mappers;
using KeyHub.Market.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace KeyHub.Market.Services.impl;

public class HomeService : IHomeService
{
    private readonly ApplicationDbContext _dbContext;

    public HomeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<GameDto>>  GetTopDiscountedGamesAsync(int count)
    { 
        var games = await _dbContext.Games
            .AsNoTracking()
            .OrderByDescending(g => g.Discount)
            .Take(count)
            .ToListAsync();

        return GameMapper.ToDtoList(games);

    }
}