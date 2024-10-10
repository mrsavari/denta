using Microsoft.AspNetCore.Mvc;
using DentalLab.Common.Models;
using SurrealDb.Net;
using System;
using System.Threading.Tasks;

namespace DentalLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreatmentsController : ControllerBase
    {
        private readonly ISurrealDbClient _dbClient;

        public TreatmentsController(ISurrealDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var treatments = await _dbClient.Select<Treatment>("treatment");
            return Ok(treatments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var treatment = await _dbClient.Select<Treatment>($"treatment:{id}");
            if (treatment == null)
                return NotFound();
            return Ok(treatment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Treatment treatment)
        {
            treatment.Id = Guid.NewGuid();
            var createdTreatment = await _dbClient.Create("treatment", treatment);
            return CreatedAtAction(nameof(GetById), new { id = createdTreatment.Id }, createdTreatment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Treatment treatment)
        {
            if (id != treatment.Id)
                return BadRequest();

            var updatedTreatment = await _dbClient.Update($"treatment:{id}", treatment);
            if (updatedTreatment == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _dbClient.Delete($"treatment:{id}");
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}