import { useState, useEffect } from "react"
import { Button } from "../ui/button"
import { Input } from "../ui/input"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../ui/table"
import { useToast } from "../ui/use-toast"

interface Invoice {
  id: string
  patientId: string
  treatmentId: string
  invoiceDate: string
  dueDate: string
  amount: number
  status: string
  paymentMethod: string
}

export function InvoiceSection() {
  const [invoices, setInvoices] = useState<Invoice[]>([])
  const [newInvoice, setNewInvoice] = useState<Partial<Invoice>>({})
  const { toast } = useToast()

  useEffect(() => {
    fetchInvoices()
  }, [])

  const fetchInvoices = async () => {
    try {
      const response = await fetch("/api/invoices")
      const data = await response.json()
      setInvoices(data)
    } catch (error) {
      console.error("Error fetching invoices:", error)
      toast({
        title: "Error",
        description: "Failed to fetch invoices",
        variant: "destructive",
      })
    }
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNewInvoice({ ...newInvoice, [e.target.name]: e.target.value })
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    try {
      const response = await fetch("/api/invoices", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newInvoice),
      })
      if (response.ok) {
        toast({ title: "Success", description: "Invoice added successfully" })
        setNewInvoice({})
        fetchInvoices()
      } else {
        throw new Error("Failed to add invoice")
      }
    } catch (error) {
      console.error("Error adding invoice:", error)
      toast({
        title: "Error",
        description: "Failed to add invoice",
        variant: "destructive",
      })
    }
  }

  return (
    <div>
      <h2 className="text-2xl font-semibold mb-4">Invoices</h2>
      <form onSubmit={handleSubmit} className="space-y-4 mb-8">
        <Input
          name="patientId"
          placeholder="Patient ID"
          value={newInvoice.patientId || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="treatmentId"
          placeholder="Treatment ID"
          value={newInvoice.treatmentId || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="invoiceDate"
          type="date"
          placeholder="Invoice Date"
          value={newInvoice.invoiceDate || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="dueDate"
          type="date"
          placeholder="Due Date"
          value={newInvoice.dueDate || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="amount"
          type="number"
          placeholder="Amount"
          value={newInvoice.amount || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="status"
          placeholder="Status"
          value={newInvoice.status || ""}
          onChange={handleInputChange}
          required
        />
        <Input
          name="paymentMethod"
          placeholder="Payment Method"
          value={newInvoice.paymentMethod || ""}
          onChange={handleInputChange}
          required
        />
        <Button type="submit">Add Invoice</Button>
      </form>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Patient ID</TableHead>
            <TableHead>Treatment ID</TableHead>
            <TableHead>Invoice Date</TableHead>
            <TableHead>Due Date</TableHead>
            <TableHead>Amount</TableHead>
            <TableHead>Status</TableHead>
            <TableHead>Payment Method</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {invoices.map((invoice) => (
            <TableRow key={invoice.id}>
              <TableCell>{invoice.patientId}</TableCell>
              <TableCell>{invoice.treatmentId}</TableCell>
              <TableCell>{new Date(invoice.invoiceDate).toLocaleDateString()}</TableCell>
              <TableCell>{new Date(invoice.dueDate).toLocaleDateString()}</TableCell>
              <TableCell>${invoice.amount.toFixed(2)}</TableCell>
              <TableCell>{invoice.status}</TableCell>
              <TableCell>{invoice.paymentMethod}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  )
}