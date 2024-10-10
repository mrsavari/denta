-- Check if the invoice table exists, if not, create it
DEFINE TABLE invoice SCHEMAFULL IF NOT EXISTS;

-- Define fields for the invoice table
DEFINE FIELD id ON invoice TYPE string IF NOT EXISTS;
DEFINE FIELD patientId ON invoice TYPE record(patient) IF NOT EXISTS;
DEFINE FIELD treatmentId ON invoice TYPE record(treatment) IF NOT EXISTS;
DEFINE FIELD invoiceDate ON invoice TYPE datetime IF NOT EXISTS;
DEFINE FIELD dueDate ON invoice TYPE datetime IF NOT EXISTS;
DEFINE FIELD amount ON invoice TYPE number IF NOT EXISTS;
DEFINE FIELD status ON invoice TYPE string IF NOT EXISTS;
DEFINE FIELD paymentMethod ON invoice TYPE string IF NOT EXISTS;
DEFINE FIELD createdAt ON invoice TYPE datetime IF NOT EXISTS;
DEFINE FIELD updatedAt ON invoice TYPE datetime IF NOT EXISTS;

-- Set up indexes for faster querying if they don't exist
DEFINE INDEX invoicePatientIndex ON invoice FIELDS patientId IF NOT EXISTS;
DEFINE INDEX invoiceTreatmentIndex ON invoice FIELDS treatmentId IF NOT EXISTS;
DEFINE INDEX invoiceDateIndex ON invoice FIELDS invoiceDate IF NOT EXISTS;