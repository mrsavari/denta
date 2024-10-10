-- Check if the treatment table exists, if not, create it
DEFINE TABLE treatment SCHEMAFULL IF NOT EXISTS;

-- Define fields for the treatment table
DEFINE FIELD id ON treatment TYPE string IF NOT EXISTS;
DEFINE FIELD patientId ON treatment TYPE record(patient) IF NOT EXISTS;
DEFINE FIELD treatmentType ON treatment TYPE string IF NOT EXISTS;
DEFINE FIELD treatmentDate ON treatment TYPE datetime IF NOT EXISTS;
DEFINE FIELD notes ON treatment TYPE string IF NOT EXISTS;
DEFINE FIELD cost ON treatment TYPE number IF NOT EXISTS;
DEFINE FIELD status ON treatment TYPE string IF NOT EXISTS;
DEFINE FIELD createdAt ON treatment TYPE datetime IF NOT EXISTS;
DEFINE FIELD updatedAt ON treatment TYPE datetime IF NOT EXISTS;

-- Set up indexes for faster querying if they don't exist
DEFINE INDEX treatmentPatientIndex ON treatment FIELDS patientId IF NOT EXISTS;
DEFINE INDEX treatmentDateIndex ON treatment FIELDS treatmentDate IF NOT EXISTS;