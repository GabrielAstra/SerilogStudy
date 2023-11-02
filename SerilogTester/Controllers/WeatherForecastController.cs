using Microsoft.AspNetCore.Mvc;

namespace SerilogTester.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            _logger.LogInformation("WeatherForeCast iniciando iLogger");
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("endpoint Get => WeatherForeCast.Get()");
            int count;

            try
            {
                for(count = 0; count <= 5; count ++)
                {
                    if (count == 3)
                        throw new Exception("Ocorreu uma Exception Aleatoria...");
                    else
                        _logger.LogInformation($"Número de iterações{count}");
                }
                var rng = new Random();

                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
            .ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"endpoit GET ->  WeatherForeCast.Get() Exception();");
                throw;
            }

            
        }
    }
}