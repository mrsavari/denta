import { useState } from "react"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "./ui/tabs"
import { PatientSection } from "./sections/PatientSection"
import { AppointmentSection } from "./sections/AppointmentSection"
import { TreatmentSection } from "./sections/TreatmentSection"
import { InvoiceSection } from "./sections/InvoiceSection"

export function Dashboard() {
  const [activeTab, setActiveTab] = useState("patients")

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-3xl font-bold mb-6">Dental Lab Management</h1>
      <Tabs value={activeTab} onValueChange={setActiveTab}>
        <TabsList>
          <TabsTrigger value="patients">Patients</TabsTrigger>
          <TabsTrigger value="appointments">Appointments</TabsTrigger>
          <TabsTrigger value="treatments">Treatments</TabsTrigger>
          <TabsTrigger value="invoices">Invoices</TabsTrigger>
        </TabsList>
        <TabsContent value="patients">
          <PatientSection />
        </TabsContent>
        <TabsContent value="appointments">
          <AppointmentSection />
        </TabsContent>
        <TabsContent value="treatments">
          <TreatmentSection />
        </TabsContent>
        <TabsContent value="invoices">
          <InvoiceSection />
        </TabsContent>
      </Tabs>
    </div>
  )
}