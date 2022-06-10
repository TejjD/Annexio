using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Annexio_Assessment.Controllers;

[ApiController]
[Route("[controller]")]
public class CountriesController : ControllerBase
{
    private static readonly HttpClient Client = new();
    private const string CountryListCacheKey = "countryList";

    private readonly IMemoryCache _cache;
    
    private readonly ILogger<CountriesController> _logger;

    public CountriesController(ILogger<CountriesController> logger, IMemoryCache cache)
    {
        _logger = logger;
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    [HttpGet(Name = "GetCountries")]
    public async Task<string?> Get()
    {
        if (_cache.TryGetValue(CountryListCacheKey, out string? msg))
        {
            _logger.Log(LogLevel.Information, "Country list found in cache");
        }
        else
        {
            _logger.Log(LogLevel.Information, "Country list not found in cache. Fetching from database");
            var stringTask = Client.GetStringAsync("https://restcountries.com/v2/all");
            msg = await stringTask;
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1024);
            _cache.Set(CountryListCacheKey, msg, cacheEntryOptions);
        }

        return msg;
    }
}