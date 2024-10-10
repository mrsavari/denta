-- Check if the patient table exists, if not, create it
DEFINE TABLE patient SCHEMAFULL IF NOT EXISTS;

-- Define fields for the patient table
DEFINE FIELD id ON patient TYPE string IF NOT EXISTS;
DEFINE FIELD name ON patient TYPE string IF NOT EXISTS;
DEFINE FIELD dateOfBirth ON patient TYPE datetime IF NOT EXISTS;
DEFINE FIELD contactNumber ON patient TYPE string IF NOT EXISTS;
DEFINE FIELD email ON patient TYPE string IF NOT EXISTS;
DEFINE FIELD address ON patient TYPE object IF NOT EXISTS;
DEFINE FIELD address.street ON patient TYPE string IF NOT EXISTS;
DEFINE FIELD address.city ON patient TYPE string IF NOT EXISTS;
DEFINE FIELD address.state ON patient TYPE string IF NOT EXISTS;
DEFINE FIELD address.zipCode ON patient TYPE string IF NOT EXISTS;
DEFINE FIELD createdAt ON patient TYPE datetime IF NOT EXISTS;
DEFINE FIELD updatedAt ON patient TYPE datetime IF NOT EXISTS;

-- Set up indexes for faster querying if they don't exist
DEFINE INDEX patientNameIndex ON patient FIELDS name IF NOT EXISTS;
DEFINE INDEX patientEmailIndex ON patient FIELDS email UNIQUE IF NOT EXISTS;