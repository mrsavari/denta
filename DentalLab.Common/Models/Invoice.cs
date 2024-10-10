using System;
using SurrealDb.Net.Models;

namespace DentalLab.Common.Models
{
    public class Invoice : Record
    {
        public Thing PatientId { get; set; }
        public Thing TreatmentId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}