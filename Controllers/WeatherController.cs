using Microsoft.AspNetCore.Mvc;

namespace SpaExt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WeatherController : ControllerBase
{
  private static readonly string[] Summaries =
  [
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  ];

  public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
  {
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
  }

  [HttpGet]
  public IEnumerable<WeatherForecast> Get()
  {
    var forecast = Enumerable.Range(1, 5).Select(index =>
    new WeatherForecast
    (
      DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
      Random.Shared.Next(-20, 55),
      Summaries[Random.Shared.Next(Summaries.Length)]
    ))
    .ToArray();

    return forecast;
  }
}
