using Microsoft.AspNetCore.Mvc;

namespace myApi.Controllers;
using myApi.models;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private static List<WeatherForecast> list;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    static WeatherForecastController(){
        list= Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToList();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return list;
    }
    [HttpGet("{id}")]
    public ActionResult<WeatherForecast> Get(int id)
    {
        if(id<0 || id>list.Count)
            return BadRequest("bad request");
        return list[id];
    }
    [HttpPost]
    public void Post (WeatherForecast newItem){
        list.Add(newItem);
    }

    [HttpPut("{id}")]
    public void Put(int id,WeatherForecast newItem){
        if(id<0||id>list.Count)
        return;
        list[id]=newItem;
    }
}
