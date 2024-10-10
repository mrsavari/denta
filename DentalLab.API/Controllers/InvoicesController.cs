using Microsoft.AspNetCore.Mvc;
using DentalLab.Common.Models;
using SurrealDb.Net;
using System;
using System.Threading.Tasks;

namespace DentalLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly ISurrealDbClient _dbClient;

        public InvoicesController(ISurrealDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _dbClient.Select<Invoice>("invoice");
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var invoice = await _dbClient.Select<Invoice>($"invoice:{id}");
            if (invoice == null)
                return NotFound();
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            var createdInvoice = await _dbClient.Create("invoice", invoice);
            return CreatedAtAction(nameof(GetById), new { id = createdInvoice.Id }, createdInvoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Invoice invoice)
        {
            if (id != invoice.Id)
                return BadRequest();

            var updatedInvoice = await _dbClient.Update($"invoice:{id}", invoice);
            if (updatedInvoice == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _dbClient.Delete($"invoice:{id}");
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}