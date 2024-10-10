using Microsoft.AspNetCore.Mvc;
using DentalLab.Common.Models;
using SurrealDb.Net;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using SystemTextJsonPatch;

namespace DentalLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreatmentsController : ControllerBase
    {
        internal const string Table = "treatment";
        private readonly ISurrealDbClient _dbClient;

        public TreatmentsController(ISurrealDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        [HttpGet]
        public Task<IEnumerable<Treatment>> GetAll(CancellationToken cancellationToken)
        {
            return _dbClient.Select<Treatment>(Table, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var treatment = await _dbClient.Select<Treatment>((Table, id), cancellationToken);

            if (treatment is null)
                return NotFound();

            return Ok(treatment);
        }

        [HttpPost]
        public Task<Treatment> Create(Treatment data, CancellationToken cancellationToken)
        {
            return _dbClient.Create(Table, data, cancellationToken);
        }

        [HttpPut]
        public Task<Treatment> Update(Treatment data, CancellationToken cancellationToken)
        {
            return _dbClient.Upsert(data, cancellationToken);
        }

        [HttpPatch]
        public Task<IEnumerable<Treatment>> PatchAll(
            JsonPatchDocument<Treatment> patches,
            CancellationToken cancellationToken
        )
        {
            return _dbClient.PatchAll(Table, patches, cancellationToken);
        }

        [HttpPatch("{id}")]
        public Task<Treatment> Patch(
            string id,
            JsonPatchDocument<Treatment> patches,
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