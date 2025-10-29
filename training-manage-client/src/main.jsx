import { StrictMode } from 'react';                // Aktivace Strict Mode
import { createRoot } from 'react-dom/client';     // React 18 root API
// import 'bootstrap/dist/css/bootstrap.min.css';
import "bootswatch/dist/flatly/bootstrap.min.css";

import './index.css';                              // Globální styly (např. Tailwind, Bootstrap)
import App from './App.jsx';                       // Hlavní komponenta celé aplikace

// Najde <div id="root"></div> v index.html a připojí aplikaci React
const container = document.getElementById('root');
if (container) {
  createRoot(container).render(
    <StrictMode>
      <App />
    </StrictMode>
  );
} else {
  console.error('Root element #root not found');
}
