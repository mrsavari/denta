using Microsoft.AspNetCore.Mvc;
using DentalLab.Common.Models;
using SurrealDb.Net;
using System;
using System.Threading.Tasks;

namespace DentalLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly ISurrealDbClient _dbClient;

        public PatientsController(ISurrealDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _dbClient.Select<Patient>("patient");
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var patient = await _dbClient.Select<Patient>($"patient:{id}");
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            patient.Id = Guid.NewGuid();
            var createdPatient = await _dbClient.Create("patient", patient);
            return CreatedAtAction(nameof(GetById), new { id = createdPatient.Id }, createdPatient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Patient patient)
        {
            if (id != patient.Id)
                return BadRequest();

            var updatedPatient = await _dbClient.Upsert(patient);
            if (updatedPatient == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _dbClient.Delete($"patient:{id}");
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}