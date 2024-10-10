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
    public class PatientsController : ControllerBase
    {
        internal const string Table = "patient";
        private readonly ISurrealDbClient _dbClient;

        public PatientsController(ISurrealDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        [HttpGet]
        public Task<IEnumerable<Patient>> GetAll(CancellationToken cancellationToken)
        {
            return _dbClient.Select<Patient>(Table, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var patient = await _dbClient.Select<Patient>((Table, id), cancellationToken);

            if (patient is null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPost]
        public Task<Patient> Create(Patient data, CancellationToken cancellationToken)
        {
            return _dbClient.Create(Table, data, cancellationToken);
        }

        [HttpPut]
        public Task<Patient> Update(Patient data, CancellationToken cancellationToken)
        {
            return _dbClient.Upsert(data, cancellationToken);
        }

        [HttpPatch]
        public Task<IEnumerable<Patient>> PatchAll(
            JsonPatchDocument<Patient> patches,
            CancellationToken cancellationToken
        )
        {
            return _dbClient.PatchAll(Table, patches, cancellationToken);
        }

        [HttpPatch("{id}")]
        public Task<Patient> Patch(
            string id,
            JsonPatchDocument<Patient> patches,
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