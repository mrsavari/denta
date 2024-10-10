import { useState, useEffect } from "react"
import { Button } from "../ui/button"
import { Input } from "../ui/input"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../ui/table"
import { useToast } from "@/hooks/use-toast"

interface Appointment {
  id: string
  patientId: string
  appointmentDate: string
  duration: string
  status: string
  notes: string
}

export function AppointmentSection() {
  const [appointments, setAppointments] = useState<Appointment[]>([])
  const [newAppointment, setNewAppointment] = useState<Partial<Appointment>>({})
  const { toast } = useToast()

  useEffect(() => {
    fetchAppointments()
  }, [])

  const fetchAppointments = async () => {
    try {
      const response = await fetch("/api/appointments")
      const data = await response.json()
      setAppointments(data)
    } catch (error) {
      console.error("Error fetching appointments:", error)
      toast({
        title: "Error",
        description: "Failed to fetch appointments",
        variant: "destructive",
      })
    }
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewAppointment({ ...newAppointment, [e.target.name]: e.target.value })
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    try {
      const response = await fetch("/api/appointments", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newAppointment),
      })
      if (response.ok) {
        toast({ title: "Success", description: "Appointment added successfully" })
        setNewAppointment({})
        fetchAppointments()
      } else {
        throw new Error("Failed to add appointment")
      }
    } catch (error) {
      console.error("Error adding appointment:", error)
      toast({
        title: "Error",
        description: "Failed to add appointment",
        variant: "destructive",
      })
    }
  }

  return (
    <div>
      <h2 className="text-2xl font-semibold mb-4">Appointments</h2>
      <form onSubmit={handleSubmit} className="space-y-4 mb-8">
        <Input
          name="patientId"
          placeholder="Patient ID"
          value={newAppointment.patientId || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="appointmentDate"
          type="datetime-local"
          placeholder="Appointment Date"
          value={newAppointment.appointmentDate || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="duration"
          placeholder="Duration (e.g., 1h30m)"
          value={newAppointment.duration || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="status"
          placeholder="Status"
          value={newAppointment.status || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="notes"
          placeholder="Notes"
          value={newAppointment.notes || ""}
          onChange={handleInputChange}
        />
        <Button type="submit">Add Appointment</Button>
      </form>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Patient ID</TableHead>
            <TableHead>Date</TableHead>
            <TableHead>Duration</TableHead>
            <TableHead>Status</TableHead>
            <TableHead>Notes</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {appointments.map((appointment) => (
            <TableRow key={appointment.id}>
              <TableCell>{appointment.patientId}</TableCell>
              <TableCell>{new Date(appointment.appointmentDate).toLocaleString()}</TableCell>
              <TableCell>{appointment.duration}</TableCell>
              <TableCell>{appointment.status}</TableCell>
              <TableCell>{appointment.notes}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  )
}