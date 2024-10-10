using System;
using SurrealDb.Net.Models;

namespace DentalLab.Common.Models
{
    public class Treatment : Record
    {
        public Thing PatientId { get; set; }
        public string TreatmentType { get; set; }
        public DateTime TreatmentDate { get; set; }
        public string Notes { get; set; }
        public decimal Cost { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}