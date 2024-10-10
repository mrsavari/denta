import { useState, useEffect } from "react"
import { Button } from "../ui/button"
import { Input } from "../ui/input"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../ui/table"
import { useToast } from "@/hooks/use-toast"

interface Treatment {
  id: string
  patientId: string
  treatmentType: string
  treatmentDate: string
  notes: string
  cost: number
  status: string
}

export function TreatmentSection() {
  const [treatments, setTreatments] = useState<Treatment[]>([])
  const [newTreatment, setNewTreatment] = useState<Partial<Treatment>>({})
  const { toast } = useToast()

  useEffect(() => {
    fetchTreatments()
  }, [])

  const fetchTreatments = async () => {
    try {
      const response = await fetch("/api/treatments")
      const data = await response.json()
      setTreatments(data)
    } catch (error) {
      console.error("Error fetching treatments:", error)
      toast({
        title: "Error",
        description: "Failed to fetch treatments",
        variant: "destructive",
      })
    }
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewTreatment({ ...newTreatment, [e.target.name]: e.target.value })
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    try {
      const response = await fetch("/api/treatments", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newTreatment),
      })
      if (response.ok) {
        toast({ title: "Success", description: "Treatment added successfully" })
        setNewTreatment({})
        fetchTreatments()
      } else {
        throw new Error("Failed to add treatment")
      }
    } catch (error) {
      console.error("Error adding treatment:", error)
      toast({
        title: "Error",
        description: "Failed to add treatment",
        variant: "destructive",
      })
    }
  }

  return (
    <div>
      <h2 className="text-2xl font-semibold mb-4">Treatments</h2>
      <form onSubmit={handleSubmit} className="space-y-4 mb-8">
        <Input
          name="patientId"
          placeholder="Patient ID"
          value={newTreatment.patientId || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="treatmentType"
          placeholder="Treatment Type"
          value={newTreatment.treatmentType || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="treatmentDate"
          type="date"
          placeholder="Treatment Date"
          value={newTreatment.treatmentDate || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="notes"
          placeholder="Notes"
          value={newTreatment.notes || ""}
          onChange={handleInputChange}
        />
        <Input
          name="cost"
          type="number"
          placeholder="Cost"
          value={newTreatment.cost || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="status"
          placeholder="Status"
          value={newTreatment.status || ""}
          onChange={handleInputChange}
          required
        />
        <Button type="submit">Add Treatment</Button>
      </form>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Patient ID</TableHead>
            <TableHead>Type</TableHead>
            <TableHead>Date</TableHead>
            <TableHead>Notes</TableHead>
            <TableHead>Cost</TableHead>
            <TableHead>Status</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {treatments.map((treatment) => (
            <TableRow key={treatment.id}>
              <TableCell>{treatment.patientId}</TableCell>
              <TableCell>{treatment.treatmentType}</TableCell>
              <TableCell>{new Date(treatment.treatmentDate).toLocaleDateString()}</TableCell>
              <TableCell>{treatment.notes}</TableCell>
              <TableCell>${treatment.cost.toFixed(2)}</TableCell>
              <TableCell>{treatment.status}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  )
}