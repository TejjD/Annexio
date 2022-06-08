using Microsoft.AspNetCore.Mvc;

namespace Annexio_Assessment.Controllers;

[ApiController]
[Route("[controller]")]
public class CountriesController : ControllerBase
{
    private static readonly HttpClient client = new();
    
    private readonly ILogger<CountriesController> _logger;

    public CountriesController(ILogger<CountriesController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCountries")]
    public async Task<string> Get()
    {
        var stringTask = client.GetStringAsync("https://restcountries.com/v2/all");

        var msg = await stringTask;

        return msg;
    }
}