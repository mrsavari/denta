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
        private readonly ISurrealDbClient _dbClient;

        public AppointmentsController(ISurrealDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _dbClient.Select<Appointment>("appointment");
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var appointment = await _dbClient.Select<Appointment>($"appointment:{id}");
            if (appointment == null)
                return NotFound();
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            var createdAppointment = await _dbClient.Create("appointment", appointment);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.Id }, createdAppointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Appointment appointment)
        {
            if (id != appointment.Id)
                return BadRequest();

            var updatedAppointment = await _dbClient.Update($"appointment:{id}", appointment);
            if (updatedAppointment == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _dbClient.Delete($"appointment:{id}");
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}