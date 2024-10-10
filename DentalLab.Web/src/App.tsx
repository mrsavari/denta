import { ThemeProvider } from "./components/theme-provider"
import { Toaster } from "./components/ui/toaster"
import { Dashboard } from "./components/Dashboard"

function App() {
  return (
    <ThemeProvider defaultTheme="light" storageKey="vite-ui-theme">
      <Dashboard />
      <Toaster />
    </ThemeProvider>
  )
}

export default App