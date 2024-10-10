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
    public class InvoicesController : ControllerBase
    {
        internal const string Table = "invoice";
        private readonly ISurrealDbClient _dbClient;

        public InvoicesController(ISurrealDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        [HttpGet]
        public Task<IEnumerable<Invoice>> GetAll(CancellationToken cancellationToken)
        {
            return _dbClient.Select<Invoice>(Table, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken)
        {
            var invoice = await _dbClient.Select<Invoice>((Table, id), cancellationToken);

            if (invoice is null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpPost]
        public Task<Invoice> Create(Invoice data, CancellationToken cancellationToken)
        {
            return _dbClient.Create(Table, data, cancellationToken);
        }

        [HttpPut]
        public Task<Invoice> Update(Invoice data, CancellationToken cancellationToken)
        {
            return _dbClient.Upsert(data, cancellationToken);
        }

        [HttpPatch]
        public Task<IEnumerable<Invoice>> PatchAll(
            JsonPatchDocument<Invoice> patches,
            CancellationToken cancellationToken
        )
        {
            return _dbClient.PatchAll(Table, patches, cancellationToken);
        }

        [HttpPatch("{id}")]
        public Task<Invoice> Patch(
            string id,
            JsonPatchDocument<Invoice> patches,
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