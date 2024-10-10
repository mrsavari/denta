using Microsoft.AspNetCore.Mvc;
using DentalLab.Common.Models;
using SurrealDb.Net;
using System;
using System.Threading.Tasks;
using SystemTextJsonPatch;

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
        public Task<IEnumerable<Appointment>> GetAll(CancellationToken cancellationToken)
        {
            return _dbClient.Select<Appointment>(Table, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var weatherForecast = await _dbClient.Select<Appointment>(
                (Table, id),
                cancellationToken
            );

            if (weatherForecast is null)
                return NotFound();

            return Ok(weatherForecast);
        }
        [HttpPost]
        public Task<Appointment> Create(Appointment data, CancellationToken cancellationToken)
        {
            return _dbClient.Create(Table, data, cancellationToken);
        }

        [HttpPut]
        public Task<Appointment> Update(Appointment data, CancellationToken cancellationToken)
        {
            return _dbClient.Upsert(data, cancellationToken);
        }

        [HttpPatch]
        public Task<IEnumerable<Appointment>> PatchAll(
            JsonPatchDocument<Appointment> patches,
            CancellationToken cancellationToken
        )
        {
            return _dbClient.PatchAll(Table, patches, cancellationToken);
        }

        /// <summary>
        /// Patches an existing weather forecast.
        /// </summary>
        [HttpPatch("{id}")]
        public Task<Appointment> Patch(
            string id,
            JsonPatchDocument<Appointment> patches,
            CancellationToken cancellationToken
        )
        {
            return _dbClient.Patch((Table, id), patches, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            bool success = await _dbClient.Delete((Table, id), cancellationToken);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}