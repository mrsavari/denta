import { useState, useEffect } from "react"
import { Button } from "../ui/button"
import { Input } from "../ui/input"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../ui/table"
import { useToast } from "@/hooks/use-toast"

interface Patient {
  id: string
  name: string
  dateOfBirth: string
  contactNumber: string
  email: string
}

export function PatientSection() {
  const [patients, setPatients] = useState<Patient[]>([])
  const [newPatient, setNewPatient] = useState<Partial<Patient>>({})
  const { toast } = useToast()

  useEffect(() => {
    fetchPatients()
  }, [])

  const fetchPatients = async () => {
    try {
      const response = await fetch("/api/patients")
      const data = await response.json()
      setPatients(data)
    } catch (error) {
      console.error("Error fetching patients:", error)
      toast({
        title: "Error",
        description: "Failed to fetch patients",
        variant: "destructive",
      })
    }
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewPatient({ ...newPatient, [e.target.name]: e.target.value })
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    try {
      const response = await fetch("/api/patients", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newPatient),
      })
      if (response.ok) {
        toast({ title: "Success", description: "Patient added successfully" })
        setNewPatient({})
        fetchPatients()
      } else {
        throw new Error("Failed to add patient")
      }
    } catch (error) {
      console.error("Error adding patient:", error)
      toast({
        title: "Error",
        description: "Failed to add patient",
        variant: "destructive",
      })
    }
  }

  return (
    <div>
      <h2 className="text-2xl font-semibold mb-4">Patients</h2>
      <form onSubmit={handleSubmit} className="space-y-4 mb-8">
        <Input
          name="name"
          placeholder="Name"
          value={newPatient.name || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="dateOfBirth"
          type="date"
          placeholder="Date of Birth"
          value={newPatient.dateOfBirth || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="contactNumber"
          placeholder="Contact Number"
          value={newPatient.contactNumber || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="email"
          type="email"
          placeholder="Email"
          value={newPatient.email || ""}
          onChange={handleInputChange}
          required
        />
        <Button type="submit">Add Patient</Button>
      </form>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Name</TableHead>
            <TableHead>Date of Birth</TableHead>
            <TableHead>Contact Number</TableHead>
            <TableHead>Email</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {patients.map((patient) => (
            <TableRow key={patient.id}>
              <TableCell>{patient.name}</TableCell>
              <TableCell>{new Date(patient.dateOfBirth).toLocaleDateString()}</TableCell>
              <TableCell>{patient.contactNumber}</TableCell>
              <TableCell>{patient.email}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  )
}