using AutoMapper;
using KeyHub.Market.data;
using KeyHub.Market.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace KeyHub.Market.Services.impl;

public class HomeService : IHomeService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public HomeService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

 
    public async Task<List<GameDto>>  GetTopDiscountedGamesAsync(int count)
    { 
        var games = await _dbContext.Games
            .AsNoTracking()
            .OrderByDescending(g => g.Discount)
            .Take(count)
            .ToListAsync();

        return _mapper.Map<List<GameDto>>(games);

    }
}