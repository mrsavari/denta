-- Check if the appointment table exists, if not, create it
DEFINE TABLE appointment SCHEMAFULL IF NOT EXISTS;

-- Define fields for the appointment table
DEFINE FIELD id ON appointment TYPE string IF NOT EXISTS;
DEFINE FIELD patientId ON appointment TYPE record(patient) IF NOT EXISTS;
DEFINE FIELD appointmentDate ON appointment TYPE datetime IF NOT EXISTS;
DEFINE FIELD duration ON appointment TYPE duration IF NOT EXISTS;
DEFINE FIELD status ON appointment TYPE string IF NOT EXISTS;
DEFINE FIELD notes ON appointment TYPE string IF NOT EXISTS;
DEFINE FIELD createdAt ON appointment TYPE datetime IF NOT EXISTS;
DEFINE FIELD updatedAt ON appointment TYPE datetime IF NOT EXISTS;

-- Set up indexes for faster querying if they don't exist
DEFINE INDEX appointmentPatientIndex ON appointment FIELDS patientId IF NOT EXISTS;
DEFINE INDEX appointmentDateIndex ON appointment FIELDS appointmentDate IF NOT EXISTS;