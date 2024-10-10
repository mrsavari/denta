using Microsoft.AspNetCore.Mvc;
using DentalLab.Common.Models;
using SurrealDb.Net;
using System;
using System.Threading.Tasks;

namespace DentalLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        internal const string Table = "appointment";
        private readonly ISurrealDbClient _dbClient;

        public AppointmentsController(ISurrealDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        [HttpGet]
        public Task<IEnumerable<WeatherForecast>> GetAll(CancellationToken cancellationToken)
        {
            return _surrealDbClient.Select<WeatherForecast>(Table, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var weatherForecast = await _surrealDbClient.Select<WeatherForecast>(
                (Table, id),
                cancellationToken
            );

            if (weatherForecast is null)
                return NotFound();

            return Ok(weatherForecast);
        }

        [HttpPost]
        public Task<WeatherForecast> Create(
            CreateWeatherForecast data,
            CancellationToken cancellationToken
        )
        {
            var weatherForecast = new WeatherForecast
            {
                Date = data.Date,
                Country = data.Country,
                TemperatureC = data.TemperatureC,
                Summary = data.Summary
            };

            return _surrealDbClient.Create(Table, weatherForecast, cancellationToken);
        }

        [HttpPut]
        public Task<WeatherForecast> Update(WeatherForecast data, CancellationToken cancellationToken)
        {
            return _surrealDbClient.Upsert(data, cancellationToken);
        }

        [HttpPatch]
        public Task<IEnumerable<WeatherForecast>> PatchAll(
            JsonPatchDocument<WeatherForecast> patches,
            CancellationToken cancellationToken
        )
        {
            return _surrealDbClient.PatchAll(Table, patches, cancellationToken);
        }

        /// <summary>
        /// Patches an existing weather forecast.
        /// </summary>
        [HttpPatch("{id}")]
        public Task<WeatherForecast> Patch(
            string id,
            JsonPatchDocument<WeatherForecast> patches,
            CancellationToken cancellationToken
        )
        {
            return _surrealDbClient.Patch((Table, id), patches, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            bool success = await _surrealDbClient.Delete((Table, id), cancellationToken);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}