using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace backend.Controllers
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
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=test;User ID=sa;Password=1;";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string query = "SELECT * FROM test";
            SqlCommand command = new SqlCommand(query, connection);

            var list = new List<WeatherForecast>();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(new WeatherForecast()
                    {
                        Summary = (string)reader["name"]
                    });
                }
            }

            connection.Close();

            return list;
        }
    }
}