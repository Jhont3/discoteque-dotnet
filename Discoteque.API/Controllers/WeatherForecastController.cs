using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers;

[ApiController]
[Route("[controller]")]
//it omtes controller, usually api/[controller]/[route] and put it again in the Name from HttpGet
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    // [HttpGet(Name = "WomenWhoCode")]
    // public async Task<IActionResult> WomenWhoCode()
    // {
    //     var name = "My name is Jhont";
    //     name += " and I like videogames";
    //     return Ok(name);
    // }
}
