import { useEffect, useState } from "react";
import heroImg from './assets/hero.png'
import reactLogo from './assets/react.svg'
import viteLogo from './assets/vite.svg'
import './App.css'

import { getHealth, type HealthStatus } from "./api/health";


function App() {
  const [health, setHealth] = useState<HealthStatus | null>(null);
  const [error, setError] = useState<string | null>(null);

 useEffect(() => {
    getHealth()
      .then(setHealth)
      .catch((err) => setError(err.message));
  }, []);

  return (
    <div>
      <h1>RetailFlow</h1>
      {error && <p style={{ color: "red" }}>Erreur : {error}</p>}
      {health && <p>API status: {health.status} — {health.timestamp}</p>}
    </div>
  );
}

export default App;