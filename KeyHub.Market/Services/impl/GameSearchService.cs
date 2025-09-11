using AutoMapper;
using KeyHub.Market.data;
using KeyHub.Market.Enums;
using KeyHub.Market.Models;
using KeyHub.Market.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace KeyHub.Market.Services.impl;

public class GameSearchService : IGameSearchService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IFilteringService _filteringService;
    private readonly ISortingService _sortingService;
    private readonly IMapper _mapper;
    public GameSearchService(ApplicationDbContext dbContext, IFilteringService filteringService,
        ISortingService sortingService, IMapper mapper)
    {
        _dbContext = dbContext;
        _filteringService = filteringService;
        _sortingService = sortingService;
        _mapper = mapper;
    }

    public (List<GameDto> Games, int TotalGames) GetSearchedGames(string? title,GameSort sortBy, Platform[]? platforms, Genre[]? genres,
        decimal? minPrice,
        decimal? maxPrice,
        int page,
        int pageSize)
    {
        IQueryable<Game> gamesQuery = GetFilteredAndSortedGames(title,sortBy, platforms, genres, minPrice, maxPrice);

        int totalGames = gamesQuery.Count();

        List<Game> games = gamesQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

     List<GameDto> mappedGames  = _mapper.Map<List<GameDto>>(games);//todo ogarnij mappera
        
        return (mappedGames, totalGames);
    }

    public IQueryable<Game> GetFilteredAndSortedGames(string? title,GameSort sortBy, Platform[]? platforms, Genre[]? genres,
        decimal? minPrice = null, decimal? maxPrice = null)
    {
        
        IQueryable<Game> games = _dbContext.Games.AsNoTracking();

        if (!string.IsNullOrEmpty(title))
        {
            games = games.Where(game => game.Title.Contains(title));
        }

        games = _filteringService.FilterByPlatform(games, platforms);
        games = _filteringService.FilterByGenres(games, genres);
        games = _filteringService.FilterByPrice(games, minPrice, maxPrice);
        games = _sortingService.SortGames(games, sortBy);

        return games;
    }
}


public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameDto>();
    }
}