using System;
using SurrealDb.Net.Models;

namespace DentalLab.Common.Models
{
    public class Appointment : Record
    {
        public string PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}